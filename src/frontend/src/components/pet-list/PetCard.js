import React from 'react';
import { FaTrash, FaEdit } from 'react-icons/fa';

const DEFAULT_PET_PHOTO = "https://img.freepik.com/premium-psd/contact-icon-illustration-isolated_23-2151903357.jpg?w=740";

function PetCard({ pet, onDelete, onEdit }) {
  return (
    <div className="bg-white shadow-lg rounded-lg p-4 hover:scale-105 transition-transform">
      <img
        src={pet.photo || DEFAULT_PET_PHOTO}
        alt={pet.name}
        className="rounded-lg w-full h-40 object-cover"
      />
      <h3 className="text-xl font-bold mt-2 text-center">Name: {pet.name}</h3>
      <p className="text-gray-600 text-center">Type: {pet.petType.name}</p>
      <p className="text-sm text-center">Age: {pet.age} 4 years</p>
      <p className="text-sm text-center">Weight: {pet.weight} 5 kg</p>
      <p className="text-sm text-center">Порода: {pet.breed || "Не указано"}</p>
      
      <div className="flex justify-between mt-3">
        <button
          onClick={() => onDelete(pet.id)}
          className="px-3"
        >
          <FaTrash className="mr-2" /> Delete
        </button>
      </div>
    </div>
  );
}

export default PetCard;
