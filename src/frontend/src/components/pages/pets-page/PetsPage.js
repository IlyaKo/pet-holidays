import { useEffect, useState } from "react";
import axios from "axios";
import { FaEdit, FaTrash, FaPlus } from "react-icons/fa";
import { API_URL } from "../../../config";

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

const DEFAULT_PET_PHOTO = "https://img.freepik.com/premium-psd/contact-icon-illustration-isolated_23-2151903357.jpg?w=740";

export default function PetsPage() {
  const [pets, setPets] = useState([]);
  const [newPet, setNewPet] = useState({ name: "", petTypeId: "", photo: "" });
  const [editingPet, setEditingPet] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchPets();
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

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewPet((prev) => ({
      ...prev,
      [name]: name === "petTypeId" ? Number(value) : value,
    }));
  };

  const handleCreateOrUpdatePet = async () => {
    setError("");
    if (!newPet.name || !newPet.petTypeId) {
      setError("Please enter the pet's name and type ID.");
      return;
    }

    try {
      if (editingPet) {
        await api.put(`/pets/${editingPet.id}`, newPet);
      } else {
        await api.post("/pets", newPet);
      }

      setNewPet({ name: "", petTypeId: "", photo: "" });
      setEditingPet(null);
      fetchPets();
    } catch (error) {
      console.error("Error saving pet:", error);
      setError("Error saving pet.");
    }
  };

  const handleEdit = (pet) => {
    setNewPet({ name: pet.name, petTypeId: pet.petTypeId, photo: pet.photo || "" });
    setEditingPet(pet);
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
      <h1 className="text-4xl font-bold text-center mb-6"></h1>

      <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
        {pets.map((pet) => (
          <div
            key={pet.id}
            className="bg-white shadow-md rounded-lg flex flex-col transition transform hover:scale-105 w-40 h-52 mx-auto"
          >
            <div className="w-full flex justify-center p-2">
              <img
                src={pet.photo || DEFAULT_PET_PHOTO}
                alt={pet.name}
                className="w-20 h-20 object-cover rounded-lg max-w-full max-h-full"
              />
            </div>
            <div className="p-2 flex flex-col justify-between flex-1 text-center">
              <div>
                <h2 className="text-sm font-semibold">{pet.name}</h2>
                <p className="text-gray-500 text-xs">Type: {pet.petTypeId}</p>
              </div>
              <div className="flex justify-center gap-2 mt-2">
                <button className="text-blue-500 hover:text-blue-700" onClick={() => handleEdit(pet)}>
                  <FaEdit size={16} />
                </button>
                <button className="text-red-500 hover:text-red-700" onClick={() => handleDelete(pet.id)}>
                  <FaTrash size={16} />
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>

      <div className="mt-10 bg-gray-100 p-6 rounded-lg shadow-md border border-gray-300">
        {error && <div className="text-red-600 font-semibold text-lg mb-4">{error}</div>}

        <h2 className="text-2xl font-semibold mb-4">{editingPet ? "Edit Pet" : "Add Pet"}</h2>

        <input
          type="text"
          name="name"
          value={newPet.name}
          onChange={handleInputChange}
          placeholder="Pet Name"
          className="border p-3 rounded w-full mb-4 text-lg shadow-sm"
        />
        <input
          type="number"
          name="petTypeId"
          value={newPet.petTypeId}
          onChange={handleInputChange}
          placeholder="Pet Type ID"
          className="border p-3 rounded w-full mb-4 text-lg shadow-sm"
        />
        <input
          type="text"
          name="photo"
          value={newPet.photo}
          onChange={handleInputChange}
          placeholder="Photo URL (optional)"
          className="border p-3 rounded w-full mb-4 text-lg shadow-sm"
        />

        <button
          onClick={handleCreateOrUpdatePet}
          className="bg-green-500 text-white px-4 py-3 rounded w-full hover:bg-green-600 text-lg flex items-center justify-center gap-2"
        >
          {editingPet ? "Save" : "Add"} <FaPlus size={20} />
        </button>
      </div>
    </div>
    </>
  );
}
