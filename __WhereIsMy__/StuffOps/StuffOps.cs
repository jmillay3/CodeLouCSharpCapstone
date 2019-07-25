using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;



namespace __WhereIsMy__.App.StuffOps
{
    public class StuffOps
    {
        private string _jsonDataDirectory;
        private string _jsonDataFile = "";
        
        public StuffOps(string JsonDataDirectory)
            {
            try
            {
                try
                {
                    Directory.CreateDirectory(JsonDataDirectory);
                    //if (Directory.Exists(JsonDataDirectory))
                
                    //get the directory containing the data file
                    _jsonDataDirectory = JsonDataDirectory;
                }
                catch
                {
                    //we couldn't find or create the data directory, throw an error
                    throw new Exception($"InstantiationError: Unable to retrieve or create the json file directory: {JsonDataDirectory}");
                }

                //derive the filename from the class
                string jsonFileName = this.GetType().Name.Replace("Ops", "") + ".json";

                //establish the path to the data file
                _jsonDataFile = $@"{JsonDataDirectory}{jsonFileName}";
            }

            catch (Exception ex)
            {
                ex.Data.Add("InstantiationError",$"An error occurred while trying to retrieve the file.");
                throw;
            }
            }

            public List<Stuffs.Stuff> GetAll()
            {
                List<Stuffs.Stuff> returnStuff = new List<Stuffs.Stuff>();

                try
                {
                    //always make sure the file exists before attempting to access it
                    if (File.Exists(_jsonDataFile))
                    {
                        //read the file
                        string stuffjson = File.ReadAllText(_jsonDataFile);

                        if (!String.IsNullOrEmpty(stuffjson))
                        {
                            returnStuff = JsonConvert.DeserializeObject<List<Stuffs.Stuff>>(stuffjson);
                        }
                    }   
                    else
                    {
                    //we couldn't find the file, create it!
                        File.Create(_jsonDataFile).Dispose(); ;
                    
                        //throw new Exception($"GetAllError: Unable to find file: {_jsonDataFile}");
                    }
                }
                catch (Exception ex)
                {
                    ex.Data.Add("GetStuffError",
                        $"An error occurred while trying to get the stuffs.");
                    throw;
                }

                return returnStuff;
            }

            public Stuffs.Stuff Add(Stuffs.Stuff stuff, List<Stuffs.Stuff> stuffs)
            {
                //get the next stuff_id
                int newStuff_ID = GetNextId(stuffs);

                //assign the stuff an id
                stuff.Stuff_ID = newStuff_ID;

                //add the stuff to the list
                stuffs.Add(stuff);

                //save the list
                Save(stuffs);

                //return the stuff with the new ID
                return stuff;
            }

            public List<Stuffs.Stuff> Delete(Stuffs.Stuff stuff, List<Stuffs.Stuff> stuffs)
            {
                try
                {
                    stuffs.Remove(stuff);
                    Save(stuffs);
                }
                catch (Exception ex)
                {
                    ex.Data.Add("DeletionError",
                        $"An error occurred while trying to delete some stuff. (Stuff ID: {stuff.Stuff_ID}");
                    throw;
                }

                return stuffs;
            }

            private void Save(List<Stuffs.Stuff> stuffs)
            {
                try
                { 
                    string stuffjson = JsonConvert.SerializeObject(stuffs);

                    if (!string.IsNullOrEmpty(stuffjson))
                    {
                        File.WriteAllText(_jsonDataFile, stuffjson);
                    }
                }
            catch (Exception ex)
                {
                    ex.Data.Add("SaveError",
                        $"An error occurred while trying to save the list.");
                    throw;
                }
            }

            private int GetNextId(List<Stuffs.Stuff> stuffs)
            {
                int returnStuff = 1;

                try
                {
                    if (stuffs.Any())
                    {
                        //get the stuff with the highest ID
                        var stuff = stuffs.OrderByDescending(u => u.Stuff_ID).FirstOrDefault();

                        //get that stuffs ID and add 1
                        int id = stuff.Stuff_ID;
                        id++;
                        returnStuff = id;
                    }
                }
                catch (Exception ex)
                {
                    ex.Data.Add("GetNextIdError",
                        "An error occurred while trying to get the new Stuff Id.");
                    throw;
                }

                return returnStuff;
            }

        }
    }


