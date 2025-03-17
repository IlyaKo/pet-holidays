import React, { useState } from 'react';
import { FaCamera } from 'react-icons/fa';

const DEFAULT_PET_PHOTO = "https://img.freepik.com/premium-psd/contact-icon-illustration-isolated_23-2151903357.jpg?w=740";

export function PetTable({ pets, onDelete, onPhotoUpload }) {
  const [hoveredPet, setHoveredPet] = useState(null);

  const handlePhotoChange = (event, petId) => {
    const file = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('file', file);
      
      onPhotoUpload(petId, formData);
    }
  };

  return (
    <table className="table is-fullwidth is-striped">
      <thead>
        <tr>
          <th style={{ width: "250px" }}>Photo</th>
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
                  src={pet.photo || DEFAULT_PET_PHOTO} 
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
            <td>3kg</td>
            <td>
              <button onClick={() => onDelete(pet.id)} className="px-3">
                Delete
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default PetTable;
