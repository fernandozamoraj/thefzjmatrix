using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TheMatrix
{
    public class IsolatedStorage
    {
        public void WriteAppSettings(string filename,
                           Settings settings)
        {
            try
            {
                IsolatedStorageFile isf =
                    System.IO.IsolatedStorage.IsolatedStorageFile.
                           GetStore(
                    IsolatedStorageScope.User |
                    IsolatedStorageScope.Assembly, null, null);
                Stream writer =
                    new IsolatedStorageFileStream(filename,
                                                  FileMode.Create, isf);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(writer, settings);
                writer.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Write Settings failed.", ex);
            }
        }

        public Settings ReadSettings(string filename)
        {
            try
            {
                IsolatedStorageFile isf =
                    System.IO.IsolatedStorage.IsolatedStorageFile.
                           GetStore(
                    IsolatedStorageScope.User |
                    IsolatedStorageScope.Assembly, null, null);
                Stream reader =
                    new IsolatedStorageFileStream(filename, FileMode.
                                                  Open, isf);
                IFormatter formatter = new BinaryFormatter();
                Settings settings = (Settings)formatter.
                                         Deserialize(reader);
                reader.Close();

                return settings;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ReadSettings failed", ex);
            }
        }
    }
}
