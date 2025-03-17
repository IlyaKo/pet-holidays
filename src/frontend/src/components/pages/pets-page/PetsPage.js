import React, { useEffect, useState } from "react";
import PetTable from "../../pet-list/PetTable";
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
  const [pets, setPets] = useState([]); 
  const [petTypes, setPetTypes] = useState([]);
  const [error, setError] = useState("");
  const [petTypeRequiredError, setPetTypeRequiredError] = useState(false);
  const [isFormVisible, setIsFormVisible] = useState(false);

  useEffect(() => {
    fetchPets();
    fetchPetTypes();
  }, []);

  const handlePhotoUpload = async (petId, formData) => {
    console.log("Uploading photo for pet:", petId);
    try {
      const uploadResponse = await api.post(`/pets/${petId}/photo`, formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
  
      if (uploadResponse.status === 200) {
        console.log("Photo uploaded successfully.");
  
        const photoResponse = await api.get(`/pets/${petId}/photo`);
        if (photoResponse.status === 200) {
          const updatedUrl = photoResponse.data.url;
          console.log("Updated photo URL:", updatedUrl);
  
          setPets((prevPets) =>
            prevPets.map((pet) =>
              pet.id === petId ? { ...pet, photo: updatedUrl } : pet
            )
          );
        } else {
          console.error("Failed to fetch updated photo URL.");
        }
      } else {
        console.error("Photo upload failed", uploadResponse);
      }
    } catch (error) {
      console.error("Error uploading or fetching updated photo:", error);
    }
  };

  const fetchPets = async () => {
    try {
      const response = await api.get("/pets");
      setPets(response.data);
    } catch (error) {
      setError("Failed to load the list of pets.");
    }
  };

  const fetchPetTypes = async () => {
    try {
      const response = await api.get("/pet-types");
      setPetTypes(response.data);
    } catch (error) {
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
      setIsFormVisible(false); 
    } catch (error) {
      setError("Error saving pet.");
    }
  };

  const handleDelete = async (id) => {
    try {
      await api.delete(`/pets/${id}`);
      fetchPets();
    } catch (error) {
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
  <PetTable pets={pets} onDelete={handleDelete} onPhotoUpload={handlePhotoUpload}/>
) : (
  <p>Loading pets...</p>
)}


<button
  onClick={() => setIsFormVisible(!isFormVisible)}
  className="button is-primary my-4"
>
  Add New Pet
</button>


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