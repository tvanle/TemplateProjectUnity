namespace AudioSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Pool;

    public class SoundManager : PersistentSingleton<SoundManager>
    {
        private          IObjectPool<SoundEmitter> soundEmitterPool;
        private readonly List<SoundEmitter>        activeSoundEmitters   = new();
        public readonly  LinkedList<SoundEmitter>  FrequentSoundEmitters = new();

        [SerializeField] private SoundEmitter soundEmitterPrefab;
        [SerializeField] private bool         collectionCheck   = true;
        [SerializeField] private int          defaultCapacity   = 10;
        [SerializeField] private int          maxPoolSize       = 100;
        [SerializeField] private int          maxSoundInstances = 30;

        private void Start() { this.InitializePool(); }

        public SoundBuilder CreateSoundBuilder() { return new SoundBuilder(this); }

        public bool CanPlaySound(SoundData data)
        {
            if (!data.frequentSound) return true;

            if (this.FrequentSoundEmitters.Count >= this.maxSoundInstances)
            {
                try
                {
                    this.FrequentSoundEmitters.First.Value.Stop();

                    return true;
                }
                catch
                {
                    Debug.Log("SoundEmitter is already released");
                }

                return false;
            }

            return true;
        }

        public SoundEmitter Get() { return this.soundEmitterPool.Get(); }

        public void ReturnToPool(SoundEmitter soundEmitter) { this.soundEmitterPool.Release(soundEmitter); }

        public void StopAll()
        {
            foreach (var soundEmitter in this.activeSoundEmitters) soundEmitter.Stop();

            this.FrequentSoundEmitters.Clear();
        }

        private void InitializePool()
        {
            this.soundEmitterPool = new ObjectPool<SoundEmitter>(this.CreateSoundEmitter, this.OnTakeFromPool, this.OnReturnedToPool, this.OnDestroyPoolObject, this.collectionCheck,
                this.defaultCapacity, this.maxPoolSize);
        }

        private SoundEmitter CreateSoundEmitter()
        {
            var soundEmitter = Instantiate(this.soundEmitterPrefab);
            soundEmitter.gameObject.SetActive(false);

            return soundEmitter;
        }

        private void OnTakeFromPool(SoundEmitter soundEmitter)
        {
            soundEmitter.gameObject.SetActive(true);
            this.activeSoundEmitters.Add(soundEmitter);
        }

        private void OnReturnedToPool(SoundEmitter soundEmitter)
        {
            if (soundEmitter.Node != null)
            {
                this.FrequentSoundEmitters.Remove(soundEmitter.Node);
                soundEmitter.Node = null;
            }

            soundEmitter.gameObject.SetActive(false);
            this.activeSoundEmitters.Remove(soundEmitter);
        }

        private void OnDestroyPoolObject(SoundEmitter soundEmitter) { Destroy(soundEmitter.gameObject); }
    }
}