import React from 'react';
import { FaTrash } from 'react-icons/fa';

const DEFAULT_PET_PHOTO = "https://img.freepik.com/premium-psd/contact-icon-illustration-isolated_23-2151903357.jpg?w=740";

function PetCard({ pet, onDelete }) {
  return (
    <div className="bg-white rounded-lg shadow-md p-4 flex flex-col items-center space-y-3 md:space-x-4">
      <div className="w-24 h-24 rounded-full overflow-hidden flex-shrink-0">
        <img
          src={pet.photo || DEFAULT_PET_PHOTO}
          alt={pet.name}
          className="w-full h-full object-contain"
        />
      </div>

      <div>
        <h3 className="text-lg font-semibold text-center">{pet.name}</h3>
        <p className="text-gray-600 text-center">{pet.petType.name}</p>
      </div>

      <div className="flex mt-2">
        <button
          onClick={() => onDelete(pet.id)}
          className="bg-red-500 hover:bg-red-600 text-white px-3 py-2 rounded-md flex items-center"
        >
          <FaTrash className="mr-2" /> Delete
        </button>
      </div>
    </div>
  );
}

export default PetCard;