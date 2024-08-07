namespace AudioSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        public SoundData                    Data { get; private set; }
        public LinkedListNode<SoundEmitter> Node { get; set; }

        private AudioSource audioSource;
        private Coroutine   playingCoroutine;

        private void Awake() { this.audioSource = this.gameObject.GetOrAdd<AudioSource>(); }

        public void Initialize(SoundData data)
        {
            this.Data                              = data;
            this.audioSource.clip                  = data.clip;
            this.audioSource.outputAudioMixerGroup = data.mixerGroup;
            this.audioSource.loop                  = data.loop;
            this.audioSource.playOnAwake           = data.playOnAwake;

            this.audioSource.mute                  = data.mute;
            this.audioSource.bypassEffects         = data.bypassEffects;
            this.audioSource.bypassListenerEffects = data.bypassListenerEffects;
            this.audioSource.bypassReverbZones     = data.bypassReverbZones;

            this.audioSource.priority      = data.priority;
            this.audioSource.volume        = data.volume;
            this.audioSource.pitch         = data.pitch;
            this.audioSource.panStereo     = data.panStereo;
            this.audioSource.spatialBlend  = data.spatialBlend;
            this.audioSource.reverbZoneMix = data.reverbZoneMix;
            this.audioSource.dopplerLevel  = data.dopplerLevel;
            this.audioSource.spread        = data.spread;

            this.audioSource.minDistance = data.minDistance;
            this.audioSource.maxDistance = data.maxDistance;

            this.audioSource.ignoreListenerVolume = data.ignoreListenerVolume;
            this.audioSource.ignoreListenerPause  = data.ignoreListenerPause;

            this.audioSource.rolloffMode = data.rolloffMode;
        }

        public void Play()
        {
            if (this.playingCoroutine != null) this.StopCoroutine(this.playingCoroutine);

            this.audioSource.Play();
            this.playingCoroutine = this.StartCoroutine(this.WaitForSoundToEnd());
        }

        private IEnumerator WaitForSoundToEnd()
        {
            yield return new WaitWhile(() => this.audioSource.isPlaying);
            this.Stop();
        }

        public void Stop()
        {
            if (this.playingCoroutine != null)
            {
                this.StopCoroutine(this.playingCoroutine);
                this.playingCoroutine = null;
            }

            this.audioSource.Stop();
            SoundManager.Instance.ReturnToPool(this);
        }

        public void WithRandomPitch(float min = -0.05f, float max = 0.05f) { this.audioSource.pitch += Random.Range(min, max); }
    }
}