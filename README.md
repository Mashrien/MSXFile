# MSXFile - VERY MUCH INCOMPLETE
MSX File Format; Google-Snappy powered, chunk-based, solid file, network-streamable, large archive file format

-- SHARED PROJECT -- not standalone. Intended to be included/embedded in another application, the UI.. or others, should you be inclined to create your own interface
(perhaps gtk for a native linux version by someone that has numbed their disgust with MonoDevelop or Rider. I'll get to it, eventually.)

Essentially a complete(ish) filesystem as a single file, organized as chunks (typically 4k) and byte-aligned as such. This makes for trivial seeking of the archive to fetch ONLY the needed chunks over a network. So rather than having to fetch an entire 40-GB archive just to pull a single 4-MB file from it, you just get the header which indicates chunk-size, pull the file table (chunkSize*numOfHeaderChunks) and that'll give a list of files, and which chunks comprise those files.

So pulling a 256-KB (262,144 byte) file from an arbitrarily large archive over the network/internet means you'll pull, at most:
```
4096-byte archive header
4096 * Number-Of-File-Table-Chunks
4096 * 65-chunks (+64-byte header in each chunk) = 256-KB
```

Though, keep in mind, with the possibility of removing files and maintaining a compact (no empty/zero space) archive, chunks may not always be sequential. So we may well have to seek to specific byte-offsets of the archive.. This is why byte-alignment is critical and integral to the format of the file structure.

Every file/directory chunk in the archive is pre-empted by a 64-byte header containing some flags as well as a unique ID to facilitate recovery of orphaned chunks in the case of a damaged or incomplete archive

For a better understanding of the headers, flags, etc, see [Enums.cs](MSX/Enums.cs) and [Structs.cs](MSX/Structs.cs)

In the future, I may add some very basic and rudimentary error checking and correction using parity bits, though this is a very long way off.. and would also dramatically increase the size of any archives made in this format. Though the trade-off may well be worth it as this is intended to be for archival storage on a NAS where disk-space is cheap and data persistence and safety is paramount.
