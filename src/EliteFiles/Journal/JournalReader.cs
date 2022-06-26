using System.Text.Json;
using EliteFiles.Internal;
using EliteFiles.Journal.Internal;

namespace EliteFiles.Journal
{
    /// <summary>
    /// Represents a reader of Elite:Dangerous player journal entries.
    /// </summary>
    /// <seealso cref="JournalEntry"/>
    public sealed class JournalReader : IDisposable
    {
        private const int _bufferSize = 131072;

        private readonly FileStream _fs;
        private readonly byte[] _buf;
        private readonly EliteFilesSerializerContext _serializerContext;

        private int _bufI;
        private int _bufN;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalReader"/> class
        /// with the specified journal file.
        /// </summary>
        /// <param name="path">The journal file to open.</param>
        public JournalReader(string path)
        {
            _fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _buf = new byte[_bufferSize];
            _serializerContext = new EliteFilesSerializerContext(new JsonSerializerOptions
            {
                Converters =
                {
                    new JournalEntryConverter(),
                },
            });
        }

        /// <summary>
        /// Gets the name of the journal file that was passed to the constructor.
        /// </summary>
        public string Name => _fs.Name;

        /// <summary>
        /// Reads a journal entry from the current journal.
        /// </summary>
        /// <returns>The journal entry, or <c>null</c> if the end of the journal has been reached.</returns>
        public JournalEntry? ReadEntry()
        {
            if (TryReadEntryFromBuffer(out JournalEntry? entry))
            {
                return entry;
            }

            CompressBuffer();

            _bufN += _fs.Read(_buf, _bufN, _buf.Length - _bufN);

            if (TryReadEntryFromBuffer(out entry))
            {
                return entry;
            }

            if (_bufN == 0)
            {
                return null;
            }

            throw new InvalidDataException($"Entry too large (greater than {_bufferSize} bytes) found in journal '{Path.GetFileName(_fs.Name)}' at position {_fs.Position - _bufN}.");
        }

        /// <summary>
        /// Releases all resources used by the <see cref="JournalReader"/>.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _fs.Dispose();
            _disposed = true;
        }

        private bool TryReadEntryFromBuffer(out JournalEntry? entry)
        {
            Span<byte> buf = _buf.AsSpan(_bufI, _bufN - _bufI);
            int i = buf.IndexOf((byte)'\n');

            if (i == -1)
            {
                entry = null;
                return false;
            }

            int n = i + 1;
            entry = JsonSerializer.Deserialize(buf[..n], _serializerContext.JournalEntry);
            _bufI += n;
            return true;
        }

        private void CompressBuffer()
        {
            int n = _bufN - _bufI;
            Array.Copy(_buf, _bufI, _buf, 0, n);
            _bufI = 0;
            _bufN = n;
        }
    }
}
