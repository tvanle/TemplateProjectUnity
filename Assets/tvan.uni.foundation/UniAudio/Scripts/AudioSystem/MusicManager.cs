namespace AudioSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Audio;

    [RequireComponent(typeof(MusicManager))]
    public class MusicManager : PersistentSingleton<MonoBehaviour>
    {
        private const    float            crossFadeTime = 1.0f;
        private          float            fading;
        private          AudioSource      current;
        private          AudioSource      previous;
        private readonly Queue<AudioClip> playlist = new();

        [SerializeField] private List<AudioClip> initialPlaylist;
        [SerializeField] private AudioMixerGroup musicMixerGroup;

        private void Start()
        {
            foreach (var clip in this.initialPlaylist) this.AddToPlaylist(clip);
        }

        public void AddToPlaylist(AudioClip clip)
        {
            this.playlist.Enqueue(clip);
            if (this.current == null && this.previous == null) this.PlayNextTrack();
        }

        public void Clear() { this.playlist.Clear(); }

        public void PlayNextTrack()
        {
            if (this.playlist.TryDequeue(out var nextTrack)) this.Play(nextTrack);
        }

        public void Play(AudioClip clip)
        {
            if (this.current && this.current.clip == clip) return;

            if (this.previous)
            {
                Destroy(this.previous);
                this.previous = null;
            }

            this.previous = this.current;

            this.current                       = this.gameObject.GetOrAdd<AudioSource>();
            this.current.clip                  = clip;
            this.current.outputAudioMixerGroup = this.musicMixerGroup; // Set mixer group
            this.current.loop                  = false; // For playlist functionality, we want tracks to play once
            this.current.volume                = 0;
            this.current.bypassListenerEffects = true;
            this.current.Play();

            this.fading = 0.001f;
        }

        private void Update()
        {
            this.HandleCrossFade();

            if (this.current && !this.current.isPlaying && this.playlist.Count > 0) this.PlayNextTrack();
        }

        private void HandleCrossFade()
        {
            if (this.fading <= 0f) return;

            this.fading += Time.deltaTime;

            var fraction = Mathf.Clamp01(this.fading / crossFadeTime);

            // Logarithmic fade
            var logFraction = fraction.ToLogarithmicFraction();

            if (this.previous) this.previous.volume = 1.0f - logFraction;
            if (this.current) this.current.volume   = logFraction;

            if (fraction >= 1)
            {
                this.fading = 0.0f;
                if (this.previous)
                {
                    Destroy(this.previous);
                    this.previous = null;
                }
            }
        }
    }
}