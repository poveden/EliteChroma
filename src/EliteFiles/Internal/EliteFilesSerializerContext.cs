using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using EliteFiles.Journal;
using EliteFiles.Journal.Events;
using EliteFiles.Status;

namespace EliteFiles.Internal
{
    [JsonSerializable(typeof(StatusEntry))]
    [JsonSerializable(typeof(JournalEntry))]
    [JsonSerializable(typeof(FileHeader))]
    [JsonSerializable(typeof(LoadGame))]
    [JsonSerializable(typeof(Music))]
    [JsonSerializable(typeof(Shutdown))]
    [JsonSerializable(typeof(StartJump))]
    [JsonSerializable(typeof(UnderAttack))]
    [ExcludeFromCodeCoverage]
    internal partial class EliteFilesSerializerContext : JsonSerializerContext
    {
    }
}
