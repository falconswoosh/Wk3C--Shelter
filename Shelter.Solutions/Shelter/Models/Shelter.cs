using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Shelter.Models
{
    public class Animal
    {
        private int _id;
        private string _name;
        private string _species;
        private string _breed;
        private int _gender;
        private int _birthday;
        private int _admission;
        private string _description;

        public void SetId(int id)
        {
            _id = id;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void SetSpecies(string species)
        {
            _species = species;
        }

        public void SetBreed(string breed)
        {
            _breed = breed;
        }

        public void SetGender(int gender)
        {
            _gender = gender;
        }

        public void SetBirthday(int birthday)
        {
            _birthday = birthday;
        }

        public void SetAdmission(int admission)
        {
            _admission = admission;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetSpecies()
        {
            return _species;
        }

        public string GetBreed()
        {
            return _breed;
        }

        public string GetDescription()
        {
            return _description;
        }

        public Animal(string name, string species, string breed, int gender, int birthday, int admission, string description)
        {
            _name = name;
            _species = species;
            _breed = breed;
            _gender = gender;
            _birthday = birthday;
            _admission = admission;
            _description = description;
        }

        public static List<Animal> GetAll()
        {
            List<Animal> animalList = new List<Animal> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM animals";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string species = rdr.GetString(2);
                string breed = rdr.GetString(3);
                int gender = rdr.GetInt32(4);
                int birthday = rdr.GetInt32(5);
                int admission = rdr.GetInt32(6);
                string description = rdr.GetString(7);
                Animal newAnimal = new Animal(name, species, breed, gender, birthday, admission, description);
                newAnimal.SetId(id);
                animalList.Add(newAnimal);

            }
            conn.Close();
            return animalList;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO animals
            (id, name, species, breed, gender, birthday, admission, description)
            VALUES (NULL, @Name, @Species, @Breed, @Gender, @Birthday, @Admission, @Description);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@Name";
            name.Value = _name;
            cmd.Parameters.Add(name);

            MySqlParameter species = new MySqlParameter();
            species.ParameterName = "@Species";
            species.Value = _species;
            cmd.Parameters.Add(species);

            MySqlParameter breed = new MySqlParameter();
            breed.ParameterName = "@Breed";
            breed.Value = _breed;
            cmd.Parameters.Add(breed);

            MySqlParameter gender = new MySqlParameter();
            gender.ParameterName = "@Gender";
            gender.Value = _gender;
            cmd.Parameters.Add(gender);

            MySqlParameter birthday = new MySqlParameter();
            birthday.ParameterName = "@Birthday";
            birthday.Value = _birthday;
            cmd.Parameters.Add(birthday);

            MySqlParameter admission = new MySqlParameter();
            admission.ParameterName = "@Admission";
            admission.Value = _admission;
            cmd.Parameters.Add(admission);

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@Description";
            description.Value = _description;
            cmd.Parameters.Add(description);

            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM animals;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
