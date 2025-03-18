import React, { useEffect, useState } from 'react';
import { FaCamera, FaTrash } from 'react-icons/fa';
import api from "../shared/api";

const DEFAULT_PET_PHOTO = "https://img.freepik.com/premium-vector/no-photo-available-vector-icon-default-image-symbol-picture-coming-soon-web-site-mobile-app_87543-14040.jpg?w=826";

export default function PetList({ pets, onDelete, onPhotoUpload }) {
  const [hoveredPet, setHoveredPet] = useState(null);
  const [petPhotos, setPetPhotos] = useState({});

  useEffect(() => {
    pets.forEach((pet) => {
      fetchPetPhoto(pet.id);
    });
  }, [pets]);

  const fetchPetPhoto = async (petId) => {
    try {
      const response = await api.get(`/pets/${petId}/photo`);
      if (response.status === 200) {
        console.log(`Fetched photo for pet ${petId}:`, response.data);
        setPetPhotos((prev) => ({ ...prev, [petId]: response.data.url }));
      }
    } catch (error) {
      console.error(`Error fetching photo for pet ${petId}:`, error);
    }
  };

  const handlePhotoChange = (event, petId) => {
    const file = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('file', file);
      onPhotoUpload(petId, formData);
    }
  };

  return (
    <>
      {pets && pets.length > 0 ? (
        <table className="table is-fullwidth is-striped">
          <thead>
            <tr>
              <th style={{ width: "300px" }}>Photo</th>
              <th>Name</th>
              <th>Type</th>
              <th>Age</th>
              <th>Weight</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {pets.map((pet) => (
              <tr key={pet.id}>
                <td>
                  <div 
                    className="image pet-photo-container" 
                    onMouseEnter={() => setHoveredPet(pet.id)}
                    onMouseLeave={() => setHoveredPet(null)}
                    style={{ position: 'relative', display: 'inline-block' }}
                  >
                    <img 
                      src={petPhotos[pet.id] || DEFAULT_PET_PHOTO} 
                      alt={pet.name || 'Pet image'} 
                      className="is-rounded pet-photo" 
                    />
                    {hoveredPet === pet.id && (
                      <label 
                        className="upload-overlay" 
                        htmlFor={`upload-photo-${pet.id}`}
                        style={{
                          position: 'absolute', 
                          top: 0, 
                          left: 0, 
                          width: '100%', 
                          height: '100%', 
                          backgroundColor: 'rgba(0, 0, 0, 0.5)', 
                          display: 'flex', 
                          alignItems: 'center', 
                          justifyContent: 'center', 
                          color: 'white', 
                          cursor: 'pointer',
                          borderRadius: '50%'
                        }}
                      >
                        <FaCamera size={24} />
                      </label>
                    )}
                    <input
                      type="file"
                      id={`upload-photo-${pet.id}`}
                      style={{ display: 'none' }}
                      onChange={(e) => handlePhotoChange(e, pet.id)}
                      accept="image/*"
                    />
                  </div>
                </td>
                <td>{pet.name}</td>
                <td>{pet.petType.name}</td>
                <td>{pet.age} years</td>
                <td>{pet.weight} kg</td>
                <td>
                  <button onClick={() => onDelete(pet.id)} className="px-3">
                    <FaTrash className="mr-2" /> Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p>Loading pets...</p>
      )}
    </>
  );
}