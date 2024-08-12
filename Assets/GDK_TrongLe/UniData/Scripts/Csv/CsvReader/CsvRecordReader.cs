namespace GDK_TrongLe.UniData.Scripts.Csv.CsvReader
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using GDK_TrongLe.UniData.Scripts.Csv.CsvReader.Converter;
    using Sylvan.Data.Csv;
    using UnityEngine;
    using MemberInfo = GDK_TrongLe.UniData.Scripts.Csv.CsvReader.Converter.MemberInfo;

    /// <summary>
    ///     Reader for a single record in a CSV file
    /// </summary>
    public class CsvRecordReader<TRecord>
    {
        private readonly Type csvType;
        private readonly Type recordType;

        private readonly List<MemberInfo> fieldsAndProperties;
        private          List<MemberInfo> blueprintCollectionMemberInfos;

        private List<ICsvCollection> listBlueprintCollections;

        public string RequireKey;

        private CustomTypeConverterAttribute customTypeConverter;

        public CsvRecordReader(Type csvType)
        {
            this.csvType             = csvType;
            this.recordType          = typeof(TRecord);
            this.fieldsAndProperties = new List<MemberInfo>();
            this.GetHeaderKey();
            this.Setup();
        }

        private void GetHeaderKey()
        {
            var csvHeaderKeyAttribute =
                (KeyAttribute)Attribute.GetCustomAttribute(this.recordType, typeof(KeyAttribute));

            //todo will remove later, should place all CsvHeaderKeyAttribute on record class instead of the blueprint class
            if (csvHeaderKeyAttribute == null)
                csvHeaderKeyAttribute =
                    (KeyAttribute)Attribute.GetCustomAttribute(this.csvType, typeof(KeyAttribute));

            if (csvHeaderKeyAttribute != null)
                this.RequireKey = csvHeaderKeyAttribute.HeaderKey;
            else
                Debug.LogError($"Not creat header key in class {this.recordType.FullName}");
        }

        private void Setup()
        {
            var memberInfos = this.recordType.GetAllFieldAndProperties();
            foreach (var memberInfo in memberInfos)
                if (this.IsBlueprintCollection(memberInfo.MemberType))
                {
                    this.blueprintCollectionMemberInfos ??= new List<MemberInfo>();
                    this.blueprintCollectionMemberInfos.Add(memberInfo);
                }
                else
                {
                    this.fieldsAndProperties.Add(memberInfo);
                }

            this.customTypeConverter = this.recordType.GetCustomAttribute<CustomTypeConverterAttribute>();
        }

        private object GetRecord(CsvDataReader inputCsv)
        {
            if (this.customTypeConverter != null)
                return this.customTypeConverter.TypeConverter.ConvertFromCsv(inputCsv);

            object record = null;

            if (!string.IsNullOrEmpty(inputCsv.GetField(this.RequireKey)))
            {
                record = Activator.CreateInstance(this.recordType);

                foreach (var memberInfo in this.fieldsAndProperties)
                    try
                    {
                        var ordinal = inputCsv.GetOrdinal(memberInfo.MemberName);
                        memberInfo.SetValue(record, inputCsv.GetField(memberInfo.MemberType, ordinal));
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        throw new FieldDontExistInBlueprint(
                            $"{this.csvType.FullName} - {inputCsv.GetField(this.RequireKey)} - {memberInfo.MemberName} : {inputCsv.GetField(memberInfo.MemberName)} - {e}");
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"{this.csvType.FullName} - {inputCsv.GetField(this.RequireKey)} - {memberInfo.MemberName} : {inputCsv.GetField(memberInfo.MemberName)} - {e}");
                    }

                if (this.blueprintCollectionMemberInfos != null)
                {
                    //Create new sub csvReader if exist
                    this.listBlueprintCollections ??= new List<ICsvCollection>();
                    this.listBlueprintCollections.Clear();

                    foreach (var subBlueprintMemberInfo in this.blueprintCollectionMemberInfos)
                    {
                        var subCollection =
                            (ICsvCollection)Activator.CreateInstance(subBlueprintMemberInfo.MemberType);
                        subBlueprintMemberInfo.SetValue(record, subCollection);
                        this.listBlueprintCollections.Add(subCollection);
                    }
                }
            }

            if (this.listBlueprintCollections != null)
                foreach (var subCollection in this.listBlueprintCollections)
                    subCollection.Add(inputCsv);

            return record;
        }

        public (bool, TRecord) IsGetRecord(CsvDataReader inputCsv)
        {
            var record = this.GetRecord(inputCsv);

            return record != null ? (true, (TRecord)record) : (false, default);
        }

        protected virtual bool IsBlueprintCollection(Type type)
        {
            return (type.IsGenericType || type.BaseType is { IsGenericType: true }) &&
                   typeof(ICsvCollection).IsAssignableFrom(type);
        }
    }
}