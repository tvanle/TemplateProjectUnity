namespace GDK_TrongLe.UniData.Scripts.Blueprint.BlueprintReader.ReaderByRow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using DataManager.Blueprint.BlueprintReader.Converter;
    using Sylvan.Data.Csv;
    using MemberInfo = DataManager.Blueprint.BlueprintReader.Converter.MemberInfo;

    public class BlueprintRecordReader
    {
        private readonly Type blueprintType;
        private readonly Type recordType;

        private readonly List<MemberInfo>                              fieldAndProperties;
        private          List<MemberInfo>                              blueprintCollectionMemberInfos;
        private          Dictionary<MemberInfo, BlueprintRecordReader> nestedMemberInfoToRecordReader;

        private List<IBlueprintCollection> listBlueprintCollections;

        public string RequireKey;

        private CustomTypeConverterAttribute customTypeConverter;

        public BlueprintRecordReader(Type blueprintType, Type recordType)
        {
            this.blueprintType      = blueprintType;
            this.recordType         = recordType;
            this.fieldAndProperties = new List<MemberInfo>();
            this.Setup();
        }

        private void Setup()
        {
            var csvHeaderKeyAttribute =
                (CsvHeaderKeyAttribute)Attribute.GetCustomAttribute(this.recordType, typeof(CsvHeaderKeyAttribute));

            //todo will remove later, should place all CsvHeaderKeyAttribute on record class instead of the blueprint class
            if (csvHeaderKeyAttribute == null) csvHeaderKeyAttribute = (CsvHeaderKeyAttribute)Attribute.GetCustomAttribute(this.blueprintType, typeof(CsvHeaderKeyAttribute));

            if (csvHeaderKeyAttribute != null)
                this.RequireKey = csvHeaderKeyAttribute.HeaderKey;

            var memberInfos = this.recordType.GetAllFieldAndProperties();
            foreach (var memberInfo in memberInfos)
                if (this.IsBlueprintCollection(memberInfo.MemberType))
                {
                    this.blueprintCollectionMemberInfos ??= new List<MemberInfo>();
                    this.blueprintCollectionMemberInfos.Add(memberInfo);
                }
                else if (this.IsBlueprintNested(memberInfo))
                {
                    this.nestedMemberInfoToRecordReader ??= new Dictionary<MemberInfo, BlueprintRecordReader>();
                    this.nestedMemberInfoToRecordReader.Add(memberInfo, new BlueprintRecordReader(memberInfo.MemberType, memberInfo.MemberType));
                }
                else
                {
                    //if require key still empty, set default is the first member name
                    if (string.IsNullOrEmpty(this.RequireKey)) this.RequireKey = memberInfo.MemberName;

                    this.fieldAndProperties.Add(memberInfo);
                }

            this.customTypeConverter = CustomAttributeExtensions.GetCustomAttribute<CustomTypeConverterAttribute>(this.recordType);
        }

        public object GetRecord(CsvDataReader inputCsv)
        {
            if (this.customTypeConverter != null)
                return this.customTypeConverter.TypeConverter.ConvertFromCsv(inputCsv);

            object record = null;

            if (!string.IsNullOrEmpty(inputCsv.GetField(this.RequireKey)))
            {
                record = Activator.CreateInstance(this.recordType);

                foreach (var memberInfo in this.fieldAndProperties)
                    try
                    {
                        var ordinal = inputCsv.GetOrdinal(memberInfo.MemberName);
                        memberInfo.SetValue(record, inputCsv.GetField(memberInfo.MemberType, ordinal));
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        throw new FieldDontExistInBlueprint(
                            $"{this.blueprintType.FullName} - {inputCsv.GetField(this.RequireKey)} - {memberInfo.MemberName} : {inputCsv.GetField(memberInfo.MemberName)} - {e}");
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"{this.blueprintType.FullName} - {inputCsv.GetField(this.RequireKey)} - {memberInfo.MemberName} : {inputCsv.GetField(memberInfo.MemberName)} - {e}");
                    }

                if (this.blueprintCollectionMemberInfos != null)
                {
                    //Create new sub blueprints if exist
                    this.listBlueprintCollections ??= new List<IBlueprintCollection>();
                    this.listBlueprintCollections.Clear();

                    foreach (var subBlueprintMemberInfo in this.blueprintCollectionMemberInfos)
                    {
                        var subCollection =
                            (IBlueprintCollection)Activator.CreateInstance(subBlueprintMemberInfo.MemberType);
                        subBlueprintMemberInfo.SetValue(record, subCollection);
                        this.listBlueprintCollections.Add(subCollection);
                    }
                }

                if (this.nestedMemberInfoToRecordReader != null)
                    foreach (var (nestedMemberInfo, recordReader) in this.nestedMemberInfoToRecordReader)
                        nestedMemberInfo.SetValue(record, recordReader.GetRecord(inputCsv));
            }
            else
            {
                if (this.nestedMemberInfoToRecordReader != null)
                    foreach (var (_, recordReader) in this.nestedMemberInfoToRecordReader)
                        recordReader.GetRecord(inputCsv);
            }

            if (this.listBlueprintCollections != null)
                foreach (var subCollection in this.listBlueprintCollections)
                    subCollection.Add(inputCsv);

            return record;
        }

        public List<List<string>> ToRawData(object inputObject, bool containHeader = false)
        {
            var result                  = new List<List<string>>();
            var notCollectionFieldCount = this.fieldAndProperties.Count;
            if (containHeader) result.Add(this.fieldAndProperties.Select(memberInfo => memberInfo.MemberName).ToList());

            var newRow = new List<string>();
            result.Add(newRow);
            foreach (var memberInfo in this.fieldAndProperties)
            {
                var converter = CsvHelper.TypeConverterCache.GetConverter(memberInfo.MemberType);
                newRow.Add(converter.ConvertToString(memberInfo.GetValue(inputObject), memberInfo.MemberType));
            }

            if (this.nestedMemberInfoToRecordReader != null)
                foreach (var (nestedMemberInfo, recordReader) in this.nestedMemberInfoToRecordReader)
                {
                    notCollectionFieldCount += recordReader.fieldAndProperties.Count;
                    var nestedObj              = nestedMemberInfo.GetValue(inputObject);
                    var nestedBlueprintRawData = recordReader.ToRawData(nestedObj, containHeader);
                    for (var i = 0; i < nestedBlueprintRawData.Count; i++) result[i].AddRange(nestedBlueprintRawData[i]);
                }

            if (this.blueprintCollectionMemberInfos != null)
                foreach (var subBlueprintMemberInfo in this.blueprintCollectionMemberInfos)
                {
                    var subBlueprintData    = (IBlueprintCollection)subBlueprintMemberInfo.GetValue(inputObject);
                    var subBlueprintRawData = subBlueprintData.ToRawData(containHeader);
                    for (var index = 0; index < subBlueprintRawData.Count; index++)
                    {
                        if (index > result.Count - 1)
                            result.Add(Enumerable.Repeat(string.Empty, notCollectionFieldCount).ToList());

                        result[index].AddRange(subBlueprintRawData[index]);
                    }
                }

            return result;
        }

        private bool IsBlueprintCollection(Type type)
        {
            return (type.IsGenericType || type.BaseType is { IsGenericType: true }) &&
                   typeof(IBlueprintCollection).IsAssignableFrom(type);
        }

        private bool IsBlueprintNested(MemberInfo typeInfo) { return typeInfo.IsDefined(typeof(NestedBlueprintAttribute)) && (typeInfo.MemberType.IsClass || typeInfo.MemberType.IsValueType); }
    }
}