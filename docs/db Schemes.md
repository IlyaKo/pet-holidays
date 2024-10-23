 # Database Schema

[See the scheme here: ](https://dbdiagram.io/e/67193a6397a66db9a309aaf1/67193ac397a66db9a309b565)
![db Schemes](https://github.com/user-attachments/assets/fdd9edad-88fa-497b-9215-ffd9b1cd2581)


 ```sql


Table Users {
  ID int [pk, increment] // Primary key, auto-incremented
  Name varchar(100) // Admin, HotelOwner, PetOwner
  Email varchar(100)
  Password varchar(100)
  Phone varchar(20)
  RegistrationDate datetime
}

Table UserRoles{
  RoleId int [ref: > Roles.ID]
  UserId int [ref: > Users.ID]
}

Table Roles {
  ID int [pk, increment] // Primary key, auto-incremented
  RoleName varchar(50) [unique] // rol name
  Description varchar(255) 
}

Table Permissions{
  ID in [pk, increment]
  RoleId int [ref: > Roles.ID]
  Description varchar(255)  
}![Untitled (1)](https://github.com/user-attachments/assets/68d4f4fd-3fd4-436e-82b4-09dcaa6116bc)


Table Hotels {
  ID int [pk, increment] // Primary key, auto-incremented
  Name varchar(100)
  Location varchar(100)
  Description text
  OwnerID int [ref: > Users.ID] // Foreign key, references Users
  Rating decimal(2,1)
}


Table Rooms {
ID int [pk, increment] // Primary key, auto-incremented
HotelID int [ref: > Hotels.ID] // Foreign key, references Hotels
Name varchar(100)
Location varchar(100)
Description text
TypeRoom int [ref: > RoomTypes.ID]
TypePet int [ref: > PetTypes.ID]
Price decimal
}

Table RoomTypes {
ID int [pk, increment]
Description text
HotelID int [ref: > Hotels.ID]
}

Table Pets {
ID int [pk, increment] // Primary key, auto-incremented
Name varchar(100)
TypeId int [ref: >PetTypes.ID]
PetOwnerId int [ref: >Users.ID]
}

Table PetTypes {
ID int [pk, increment] // Primary key, auto-incremented
Name varchar(100)
}

Table Reviews {
  ID int [pk, increment] // Primary key, auto-incremented
  HotelID int [ref: > Hotels.ID] // Foreign key, references Hotels
  UserID int [ref: > Users.ID] // Foreign key, references Users
  ReviewText text
  Rating decimal(1,0)
  ReviewDate datetime
  FileId int [ref: > Files.ID]
}


Table Bookings {
  ID int [pk, increment] // Primary key, auto-incremented
  UserID int [ref: > Users.ID] // Foreign key, references Users (Pet Owners)
  RoomID int [ref: > Rooms.ID] // Foreign key, references Hotels
  CheckInDate datetime
  CheckOutDate datetime
  PetId int [ref: > Pets.ID]
  BookingStatus int // Created, Confirmed, Cancelled
}

Table Chats {
  ID int [pk, increment] // Primary key, auto-incremented
  HotelID int [ref: > Hotels.ID] // Foreign key, references Hotels
  PetOwner int [ref: > Users.ID]
}

Table Messages {
  ID int
  ChatID int [ref: > Chats.ID]
  MessageText text
  MessageDate datetime
  Read bool
  SenderID int [ref: > Users.ID] 
  ReceiverID int [ref: > Users.ID] 
  FileId int [ref: > Files.ID]
}

Table Files {
  ID int [pk, increment] // Primary key, auto-incremented
  UserID int [ref: > Users.ID] // Foreign key, references Users
  FileName varchar(255)
  FilePath varchar(255)
  UploadDate datetime
}

Table Notifications {
  ID int [pk, increment] // Primary key, auto-incremented
  UserID int [ref: > Users.ID] // Foreign key, references Users
  NotificationText text
  NotificationDate datetime
  ReadStatus boolean // True = Read, False = Unread
}
 ```