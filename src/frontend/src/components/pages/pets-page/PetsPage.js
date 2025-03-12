import React, { useEffect, useState } from "react";
import PetCard, { PetTable } from "../../pet-list/PetTable";
import AddPetForm from "../../pet-list/AddPetForm"; 
import api from "../../shared/api";

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

      {error && (
        <div className="alert alert-error mb-4">
          <p>{error}</p>
        </div>
      )}
 

 {pets && pets.length > 0 ? (
  <PetTable pets={pets} onDelete={handleDelete} />
) : (
  <p>Loading pets...</p>
)}


        <button
          onClick={() => setIsFormVisible(!isFormVisible)}
          className="button is-primary my-4"
        >
          <button /> Add New Pet  </button>


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