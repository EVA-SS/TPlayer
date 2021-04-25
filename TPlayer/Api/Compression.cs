using SharpCompress.Common;
using SharpCompress.Compressors;
using SharpCompress.Compressors.Deflate;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;

namespace TPlayer
{
    public class Compression
    {
        public static bool Decompression(string path, string dir, out string err)
        {
            try
            {
                using (Stream stream = File.OpenRead(path))
                using (var reader = ReaderFactory.Open(stream))
                {
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            Console.WriteLine(reader.Entry.Key);
                            reader.WriteEntryToDirectory(dir, new ExtractionOptions
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                    }
                }
                err = null;
                return true;
            }
            catch (Exception ez) { err = ez.Message; }
            return false;
        }
        public static List<string> DecompressionList(string path, string dir, out string err)
        {
            try
            {
                List<string> files = new List<string>();
                using (Stream stream = File.OpenRead(path))
                using (var reader = ReaderFactory.Open(stream))
                {
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory && reader.Entry.Key != null)
                        {
                            files.Add(reader.Entry.Key);
                            Console.WriteLine(reader.Entry.Key);
                            reader.WriteEntryToDirectory(dir, new ExtractionOptions
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                    }
                }
                err = null;
                return files;
            }
            catch (Exception ez) { err = ez.Message; }
            return null;
        }
        public static string Decompress(string path, string dir, string pathName, out string err)
        {
            try
            {
                using (FileStream originalFileStream = File.OpenRead(path))
                {
                    string newFileName = pathName.Remove(pathName.Length - Path.GetExtension(pathName).Length);

                    using (FileStream decompressedFileStream = File.Create(dir + newFileName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            MemoryStream outBuffer = new MemoryStream();
                            byte[] block = new byte[1024];
                            while (true)
                            {
                                int bytesRead = decompressionStream.Read(block, 0, block.Length);
                                if (bytesRead <= 0)
                                    break;
                                else
                                    outBuffer.Write(block, 0, bytesRead);
                            }

                            byte[] bby = outBuffer.ToArray();
                            decompressionStream.CopyTo(decompressedFileStream);
                            decompressedFileStream.Write(bby, 0, bby.Length);
                            Console.WriteLine("Decompressed: {0}", newFileName);
                            err = null;
                            return dir + newFileName;
                        }
                    }
                }
            }
            catch (Exception ez) { err = ez.Message; }
            return null;
        }


    }
}
