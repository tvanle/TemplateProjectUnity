namespace AudioSystem
{
    using UnityEngine;

    public class SoundBuilder
    {
        private readonly SoundManager soundManager;
        private          Vector3      position = Vector3.zero;
        private          bool         randomPitch;

        public SoundBuilder(SoundManager soundManager) { this.soundManager = soundManager; }

        public SoundBuilder WithPosition(Vector3 position)
        {
            this.position = position;

            return this;
        }

        public SoundBuilder WithRandomPitch()
        {
            this.randomPitch = true;

            return this;
        }

        public void Play(SoundData soundData)
        {
            if (soundData == null)
            {
                Debug.LogError("SoundData is null");

                return;
            }

            if (!this.soundManager.CanPlaySound(soundData)) return;

            var soundEmitter = this.soundManager.Get();
            soundEmitter.Initialize(soundData);
            soundEmitter.transform.position = this.position;
            soundEmitter.transform.parent   = this.soundManager.transform;

            if (this.randomPitch) soundEmitter.WithRandomPitch();

            if (soundData.frequentSound) soundEmitter.Node = this.soundManager.FrequentSoundEmitters.AddLast(soundEmitter);

            soundEmitter.Play();
        }
    }
}