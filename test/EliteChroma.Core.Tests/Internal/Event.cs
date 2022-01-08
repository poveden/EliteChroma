namespace EliteChroma.Core.Tests.Internal
{
    internal sealed class Event
    {
        public Event(bool isStatus, string json, bool changesGameState)
        {
            IsStatus = isStatus;
            Json = json;
            ChangesGameState = changesGameState;
        }

        public bool IsStatus { get; }

        public string Json { get; }

        public bool ChangesGameState { get; }
    }
}
