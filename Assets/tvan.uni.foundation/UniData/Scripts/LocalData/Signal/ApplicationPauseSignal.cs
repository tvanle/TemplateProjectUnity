namespace tvan.uni.foundation.UniData.Scripts.LocalData.Signal
{
    public class ApplicationPauseSignal
    {
        public bool PauseStatus { get; set; }

        public ApplicationPauseSignal(bool pauseState) { this.PauseStatus = pauseState; }
    }
}