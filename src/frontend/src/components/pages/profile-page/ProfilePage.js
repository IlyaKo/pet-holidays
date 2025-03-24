import React, { useEffect, useState } from "react";
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

export default function ProfilePage() {
  const [userProfile, setUserProfile] = useState(null);
  const [userID, setUserID] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);
  const [updating, setUpdating] = useState(false);

  const fetchProfileUser = async () => {
    try {
      const response = await api.get("/users/current");
      setUserProfile(response.data);
    } catch (error) {
      setError("Failed to load the User information.");
    } finally {
      setLoading(false);
    }
  };

  const fetchUserID = async () => {
    try {
      const response = await api.get("users/profile");
      setUserID(response.data.userId);
    } catch (error) {
      setError("Failed to load the UserId.");
    }
  };

  useEffect(() => {
    fetchUserID();
    fetchProfileUser();
  }, []);

  const handleSave = async () => {
    setUpdating(true);
    try {
      await api.put(`/user/update/${userID}`, userProfile);
      alert("Profile updated successfully");
    } catch (error) {
      console.error("Error updating profile", error);
      alert("Failed to update profile");
    } finally {
      setUpdating(false);
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error || !userProfile) {
    return <div>{error || "User data not available"}</div>;
  }

  return (
    <div className="m-2">
      <h2 className="title mt-4">Edit Profile</h2>
      <div className="columns">
        <div className="column is-one-quarter">
          <div className="field">
            <label className="label">Username</label>
            <div className="control">
              <input
                className="input"
                type="text"
                value={userProfile.userName}
                onChange={(e) =>
                  setUserProfile({ ...userProfile, userName: e.target.value })
                }
              />
            </div>
          </div>

          <div className="field">
            <label className="label">Email</label>
            <div className="control">
              <input
                className="input"
                type="email"
                value={userProfile.email}
                onChange={(e) =>
                  setUserProfile({ ...userProfile, email: e.target.value })
                }
              />
            </div>
          </div>

          <div className="field">
            <label className="label">Phone Number</label>
            <div className="control">
              <input
                className="input"
                type="text"
                value={userProfile.phoneNumber}
                onChange={(e) =>
                  setUserProfile({
                    ...userProfile,
                    phoneNumber: e.target.value,
                  })
                }
              />
            </div>
          </div>

          <button
            className="button is-primary"
            onClick={handleSave}
            disabled={updating}
          >
            {updating ? "Saving..." : "Save Changes"}
          </button>
        </div>
      </div>
    </div>
  );
}
