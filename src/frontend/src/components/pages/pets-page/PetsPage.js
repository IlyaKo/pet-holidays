import React, { useEffect, useState } from "react";
import axios from "axios";
import { API_URL } from "../../../config";
import PetCard from "../../pet-list/PetCard";
import AddPetForm from "../../pet-list/AddPetForm"; 
import { FaPlus } from "react-icons/fa"; 



const api = axios.create({
  baseURL: `${API_URL}`,
});

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export default function PetsPage() {
  const [pets, setPets] = useState(null);
  const [error, setError] = useState("");
  const [petTypes, setPetTypes] = useState([]);
  const [petTypeRequiredError, setPetTypeRequiredError] = useState(false);
  const [isFormVisible, setIsFormVisible] = useState(false);

  useEffect(() => {
    fetchPets();
    fetchPetTypes();
  }, []);

  const fetchPets = async () => {
    try {
      const response = await api.get("/pets");
      setPets(response.data);
    } catch (error) {
      console.error("Error loading pets:", error);
      setError("Failed to load the list of pets.");
    }
  };

  const fetchPetTypes = async () => {
    try {
      const response = await api.get("/pet-types");
      setPetTypes(response.data);
    } catch (error) {
      console.error("Error loading pet types:", error);
      setError("Failed to load the list of pet types.");
    }
  };

  const handleCreatePet = async (newPet) => {
    if (!newPet.petTypeId) {
      setPetTypeRequiredError(true);
      return;
    }
    setPetTypeRequiredError(false);
    try {
      await api.post("/pets", newPet);
      fetchPets();
      setIsFormVisible(false); // Закрываем форму после успешного добавления
    } catch (error) {
      console.error("Error saving pet:", error);
      setError("Error saving pet.");
    }
  };

  const handleDelete = async (id) => {
    try {
      await api.delete(`/pets/${id}`);
      fetchPets();
    } catch (error) {
      console.error("Error deleting pet:", error);
      setError("Error deleting pet.");
    }
  };

  return (
    <>
      <h2 className="title m-4">My Pets</h2>
      <hr />
      <div className="max-w-2xl mx-auto p-6">
 

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {pets && pets.length > 0 ? (
            pets.map((pet) => (
              <PetCard key={pet.id} pet={pet} onDelete={handleDelete} />
            ))
          ) : (
            <p>Loading pets...</p>
          )}
        </div>


        <button
          onClick={() => setIsFormVisible(!isFormVisible)}
          className="bg-gradient-to-r from-blue-500 to-blue-600 hover:from-blue-600 hover:to-blue-700 text-white font-semibold py-2 px-4 rounded-lg shadow-md transition duration-300 ease-in-out transform hover:scale-105 flex items-center gap-2"
        >
          <FaPlus /> Add New Pet  </button>


        {isFormVisible && (
          <AddPetForm
            onAddPet={handleCreatePet}
            petTypes={petTypes}
            petTypeRequiredError={petTypeRequiredError}
            setIsFormVisible={setIsFormVisible}
          />
        )}
      </div>
    </>
  );
}