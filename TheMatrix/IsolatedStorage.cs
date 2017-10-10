using System;
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
                //stopped using IsolatedStorage becasue no matter how hard I tried it was sketchy
                //and did not read it..
                //it would read and write when writing settings but it would not read when the app ran
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string filePath = Path.Combine(folderPath, filename);

                using(BinaryWriter br = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate)) )
                {
                    Stream writer = br.BaseStream;

                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(writer, settings);
                    writer.Close();    
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Write Settings failed.", ex);
            }
        }

        public Settings ReadSettings(string filename)
        {
            //stopped using IsolatedStorage becasue no matter how hard I tried it was sketchy
            //and did not read it..
            //it would read and write when writing settings but it would not read when the app ran
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(folderPath, filename);

            Settings settings = null;

            using(BinaryReader br = new BinaryReader(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                Stream reader = br.BaseStream;         
                IFormatter formatter = new BinaryFormatter();
                settings = (Settings)formatter.Deserialize(reader);
                reader.Close();
            }

            return settings;
        }
    }
}
