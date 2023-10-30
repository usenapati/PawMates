
USE PawMates;
GO
SET IDENTITY_INSERT PetParents ON
INSERT INTO PetParents(Id, FirstName, LastName, Email, PhoneNumber, ImageUrl) VALUES (1, 'Baxter', 'Smith', 'bsmith72@gmail.com','258-700-8873', 'https://www.pexels.com/photo/man-wearing-white-dress-shirt-and-black-blazer-2182970/');
INSERT INTO PetParents(Id, FirstName, LastName, Email, PhoneNumber, ImageUrl) VALUES (2, 'Susette', 'Cisneros', 'scisneros1@shutterfly.com', '964-868-7743', 'https://www.pexels.com/photo/woman-smiling-at-the-camera-1181686/');
INSERT INTO PetParents(Id, FirstName, LastName, Email, PhoneNumber, ImageUrl) VALUES (3, 'Nalani', 'Janet', 'njanet2@go.com', '767-123-3952', 'https://www.pexels.com/photo/smiling-black-model-with-afro-braids-on-gray-background-7275385/');
INSERT INTO PetParents(Id, FirstName, LastName, Email, PhoneNumber, ImageUrl) VALUES (4, 'Ardith', 'Kelsell', 'akelsell3@mac.com', '240-432-6444', 'https://www.pexels.com/photo/man-wearing-eyeglasses-769772/');
INSERT INTO PetParents(Id, FirstName, LastName, Email, PhoneNumber, ImageUrl) VALUES (5, 'Lionel', 'Bruins', 'lbruins@yahoo.com', '739-221-7106', 'https://www.pexels.com/photo/photo-of-man-wearing-eyeglasses-6801642/');
INSERT INTO PetParents(Id, FirstName, LastName, Email, PhoneNumber, ImageUrl) VALUES (6, 'Gaia', 'Elsbury', 'gelsbury0@instagram.com','417-125-1151', 'https://www.pexels.com/photo/closeup-photo-of-smiling-woman-wearing-blue-denim-jacket-1130626/');
INSERT INTO PetParents(Id, FirstName, LastName, Email, PhoneNumber, ImageUrl) VALUES (7, 'Farah', 'Abadi', 'fabadi1@seesaa.net', '232-828-2153', 'https://www.pexels.com/photo/woman-s-in-red-sari-portrait-764529/');
INSERT INTO PetParents(Id, FirstName, LastName, Email, PhoneNumber, ImageUrl) VALUES (8, 'Jennifer', 'Chen', 'jchen2@wikia.com', '922-597-5043', 'https://www.pexels.com/photo/woman-smiling-2709388/');
INSERT INTO PetParents(Id, FirstName, LastName, Email, PhoneNumber, ImageUrl) VALUES (9, 'David', 'Nguyen', 'dnguyen3@gmail.com', '532-914-7265', 'https://www.pexels.com/photo/handsome-young-ethnic-guy-looking-at-camera-and-smiling-5384445/');
INSERT INTO PetParents(Id, FirstName, LastName, Email, PhoneNumber, ImageUrl) VALUES (10, 'Markos', 'Martinez', 'mmartinez4@abc.net', '453-561-0533', 'https://www.pexels.com/photo/man-wearing-blue-crew-neck-t-shirt-2379005/');
SET IDENTITY_INSERT PetParents OFF
SET IDENTITY_INSERT Users ON
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (1, null, 'admin', 'admin');
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (2, 1, 'bsmith72', 'X)EcRaUmE}=tWgq');
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (3, 2, 'scisneros1', 'gP~WjDw#B}[CsS=');
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (4, 3, 'njanet2', ']SW&hyp#6}!rR+n');
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (5, 4, 'akelsell3', 'ReHvasMIL8VciAZ');
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (6, 5, 'lbruins', 'hUp#-qx(PgMg!0G');
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (7, 6, 'gelsbury0', 'Fk}JZ0mJz+dsPuv');
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (8, 7, 'fabadi1', 'dN8#2iDP,?+4%3Lw');
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (9, 8, 'jchen2', 'pb60}mRl%kJgz0J');
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (10, 9, 'dnguyen3', '[jg=9nDIz#,A-TX');
INSERT INTO Users(Id, PetParentId, Username, [Password]) VALUES (11, 10, 'mmartinez4', 'Pkk_u3&p^Vps;dl');
SET IDENTITY_INSERT Users OFF
SET IDENTITY_INSERT RestrictionTypes ON
INSERT INTO RestrictionTypes(Id, [Name]) VALUES (1, 'Age Restriction: Must be under 2 years old.');
INSERT INTO RestrictionTypes(Id, [Name]) VALUES (2, 'Age Restriction: Must be at least 2 years old.');
INSERT INTO RestrictionTypes(Id, [Name]) VALUES (3, 'Age Restriction: Senior Animals Only');
INSERT INTO RestrictionTypes(Id, [Name]) VALUES (4, 'Pet Type Restriction: Cats Only');
INSERT INTO RestrictionTypes(Id, [Name]) VALUES (5, 'Pet Type Restriction: Dogs Only');
INSERT INTO RestrictionTypes(Id, [Name]) VALUES (6, 'Pet Type Restriction: Birds Only');
INSERT INTO RestrictionTypes(Id, [Name]) VALUES (7, 'Pet Type Restriction: Reptiles Only');
INSERT INTO RestrictionTypes(Id, [Name]) VALUES (8, 'Pet Type Restriction: Rabbits Only');
INSERT INTO RestrictionTypes(Id, [Name]) VALUES (9, 'Breed Restriction');
SET IDENTITY_INSERT RestrictionTypes OFF
SET IDENTITY_INSERT EventTypes ON
INSERT INTO EventTypes(Id, RestrictionTypeId, PetTypeId, [Name], [Description]) VALUES (1, 5, 3, 'Bark ''n'' Splash Day', 'A water-themed play date at a dog-friendly pool or a pet water park where dogs can splash, swim, and play fetch in the water. Perfect for cooling off on hot summer days.');
INSERT INTO EventTypes(Id, RestrictionTypeId, PetTypeId,  [Name], [Description]) VALUES (2, 4, 2, 'Purrfect Picnic in the Park', 'An outdoor play date for cats and their owners, featuring cozy blankets, shade, and a variety of interactive toys. Cats can explore, relax, and enjoy a gourmet picnic.');
INSERT INTO EventTypes(Id, RestrictionTypeId, PetTypeId,  [Name], [Description]) VALUES (3, null, 1, 'Pet Yoga & Relaxation Retreat', 'A holistic play date for all pets, including gentle yoga sessions for pets and their owners, followed by relaxation exercises, massages, and soothing music.');
INSERT INTO EventTypes(Id, RestrictionTypeId, PetTypeId,  [Name], [Description]) VALUES (4, null, 1, 'Paw-ty in the Park', 'A lively play date with a pet-friendly DJ, games, and pet-themed snacks. Pets can strut their stuff on a makeshift runway and enjoy a pet costume contest.');
INSERT INTO EventTypes(Id, RestrictionTypeId, PetTypeId,  [Name], [Description]) VALUES (5, 5, 3, 'Trail Trek & Tails Wag', 'An adventure play date for dogs, featuring a group hike on scenic trails with plenty of sniffing opportunities. A great way for dogs to explore the outdoors and socialize.');
INSERT INTO EventTypes(Id, RestrictionTypeId, PetTypeId,  [Name], [Description]) VALUES (6, 6, 4, 'Beak ''n'' Treat Tweetup', 'A casual meet-up for bird owners to share bird stories, birdie snacks, and the joy of avian companionship.');
INSERT INTO EventTypes(Id, RestrictionTypeId, PetTypeId,  [Name], [Description]) VALUES (7, null, 1, 'Pawtographer''s Portrait Playdate', 'A delightful day dedicated to capturing the special bond between pets and their owners through the art of photography.');
INSERT INTO EventTypes(Id, RestrictionTypeId, PetTypeId,  [Name], [Description]) VALUES (8, 2, 1, 'Pet Parkour & Agility Adventure', 'An action-packed play date where dogs can test their agility skills on an obstacle course. Great for exercise and mental stimulation.');
INSERT INTO EventTypes(Id, RestrictionTypeId, PetTypeId,  [Name], [Description]) VALUES (9, 8, 6, 'Bunny Bounce & Hop Fest', 'A fun play date for rabbits and their owners, featuring a safe enclosed space where bunnies can hop, play, and munch on fresh greens.');
INSERT INTO EventTypes(Id, RestrictionTypeId, PetTypeId,  [Name], [Description]) VALUES (10, 7, 5,'Reptile Reunion & Sun-soaking Soiree', 'A unique play date for reptile enthusiasts, with separate enclosures for various reptiles and an educational component to learn more about these fascinating creatures.');
SET IDENTITY_INSERT EventTypes OFF
SET IDENTITY_INSERT PetTypes ON
INSERT INTO PetTypes(Id, Species) VALUES (1, 'Any');
INSERT INTO PetTypes(Id, Species) VALUES (2, 'Cat');
INSERT INTO PetTypes(Id, Species) VALUES (3, 'Dog');
INSERT INTO PetTypes(Id, Species) VALUES (4, 'Bird');
INSERT INTO PetTypes(Id, Species) VALUES (5, 'Reptile');
INSERT INTO PetTypes(Id, Species) VALUES (6, 'Rabbit');
SET IDENTITY_INSERT PetTypes OFF
SET IDENTITY_INSERT Locations ON
INSERT INTO Locations(Id, PetTypeId, [Name], Street1, City, [State], PostalCode, PetAge) VALUES (1, 1, 'The Critter Collective', '123 Bellvue', 'Durham', 'NC', '27701',1);
INSERT INTO Locations(Id, PetTypeId, [Name], Street1, City, [State], PostalCode, PetAge) VALUES (2, 3, 'Skiptown', '222 Rampart St', 'Charlotte', 'NC', '28203', 2);
INSERT INTO Locations(Id, PetTypeId, [Name], Street1, City, [State], PostalCode, PetAge) VALUES (3, 2, 'Crooked Tail Cat Cafe', '604 S Elm St', 'Greensboro', 'NC', '27406', 2);
INSERT INTO Locations(Id, PetTypeId, [Name], Street1, City, [State], PostalCode, PetAge) VALUES (4, 3, 'West Street Dog', '400 W. North Street Unit 110', 'Raleigh', 'NC', '27603', 1);
INSERT INTO Locations(Id, PetTypeId, [Name], Street1, City, [State], PostalCode, PetAge) VALUES (5, 3, 'Ruff Draft Dog Park & Bar', '2144 Wrightsville Avenue', 'Wilmington', 'NC', '28403', 3);
INSERT INTO Locations(Id, PetTypeId, [Name], Street1, City, [State], PostalCode, PetAge) VALUES (6, 2, 'Crooked Tail Cat Cafe', '229 W. Fifth St', 'Winston-Salem', 'NC', '27101', 0);
SET IDENTITY_INSERT Locations OFF
SET IDENTITY_INSERT Pets ON
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (1, 1, 3, 'Sadie', 'Golden Retriever', 1, '27603', 'https://images.pexels.com/photos/2759564/pexels-photo-2759564.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (2, 2, 2, 'Charlie', null, 2, '28203', 'https://images.pexels.com/photos/977935/pexels-photo-977935.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (3, 2, 2, 'Oliver', null, 2, '28203', 'https://images.pexels.com/photos/1741205/pexels-photo-1741205.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (4, 3, 6, 'Daisy', 'Holland Lop', 3, '27701', 'https://images.pexels.com/photos/6897434/pexels-photo-6897434.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (5, 4, 3, 'Bud', 'Dachshund', 6, '28403', 'https://images.pexels.com/photos/1139794/pexels-photo-1139794.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (6, 4, 3, 'Tala', 'Dachshund', 1, '28403', 'https://images.pexels.com/photos/6399508/pexels-photo-6399508.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (7, 5, 4, 'Ollin', 'Parrot', 6, '27701', 'https://images.pexels.com/photos/2725982/pexels-photo-2725982.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (8, 6, 4, 'Dina', 'Parakeet', 2, '27101', 'https://images.pexels.com/photos/1661179/pexels-photo-1661179.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (9, 7, 5, 'Godzilla', 'Bearded Dragon', 4, '27101', 'https://images.pexels.com/photos/7322660/pexels-photo-7322660.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (10, 8, 2, 'Bella', null, 2, '27406', 'https://images.pexels.com/photos/982300/pexels-photo-982300.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (11, 8, 2, 'Mr. Rogers', 'Ragdoll', 2, '27406', 'https://images.pexels.com/photos/18691942/pexels-photo-18691942/free-photo-of-photo-of-a-ragdoll-cat.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (12, 8, 2, 'Bruce Wayne', 'Ragdoll', 2, '27406', 'https://images.pexels.com/photos/7057542/pexels-photo-7057542.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (13, 9, 5, 'Rex', 'Bearded Dragon', 6, '27701', 'https://images.pexels.com/photos/15396817/pexels-photo-15396817/free-photo-of-bearded-dragon-in-close-up.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (14, 9, 5, 'Gizmo', 'Bearded Dragon', 7, '27701', 'https://images.pexels.com/photos/4712871/pexels-photo-4712871.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
INSERT INTO Pets(Id, PetParentId, PetTypeId, [Name], Breed, Age, PostalCode, ImageUrl) VALUES (15, 10, 6, 'Nana', 'Holland Lop', 2, '27701', 'https://images.pexels.com/photos/6957537/pexels-photo-6957537.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
SET IDENTITY_INSERT Pets OFF
SET IDENTITY_INSERT PlayDates ON
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (1, 1, 4, 8, '2023-06-11 10:00:00', '2023-06-11 16:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (2, 1, 4, 5, '2023-11-15 10:00:00', '2023-11-15 15:30:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (3, 2, 2, 3, '2023-11-18 08:00:00', '2023-11-18 14:30:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (4, 2, 2, 7, '2023-11-27 09:00:00', '2023-11-27 13:30:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (5, 3, 1, 9, '2023-12-05 10:00:00', '2023-12-05 17:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (6, 4, 5, 4, '2023-07-17 10:00:00', '2023-07-17 16:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (7, 4, 5, 5, '2023-08-30 09:30:00', '2023-08-30 16:30:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (8, 4, 5, 1, '2023-09-13 10:00:00', '2023-09-13 16:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (9, 4, 5, 3, '2023-10-18 08:00:00', '2023-10-18 13:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (10, 5, 1, 6, '2023-11-01 08:45:00', '2023-11-01 16:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (11, 6, 1, 6, '2023-08-03 09:00:00', '2023-08-03 16:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (12, 7, 1, 3, '2023-12-10 10:00:00', '2023-12-10 16:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (13, 7, 1, 10, '2023-10-06 10:00:00', '2023-10-06 15:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (14, 8, 3, 2, '2023-12-14 08:00:00', '2023-12-14 12:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (15, 8, 3, 3, '2023-12-28 10:00:00', '2023-12-28 17:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (16, 8, 3, 4, '2023-11-11 09:00:00', '2023-11-11 13:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (17, 8, 3, 7, '2023-11-25 08:30:00', '2023-11-25 13:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (18, 9, 1, 3, '2023-11-21 09:00:00', '2023-11-21 17:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (19, 9, 1, 10, '2023-11-15 08:00:00', '2023-11-15 13:00:00');
INSERT INTO PlayDates(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime) VALUES (20, 10, 1, 9, '2023-11-28 10:00:00', '2023-11-28 17:00:00');
SET IDENTITY_INSERT PlayDates OFF
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (1, 1);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (1, 6);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (2, 1);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (2, 5);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (3, 2);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (3, 3);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (3, 10);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (3, 11);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (3, 12);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (4, 2);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (4, 10);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (4, 11);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (4, 12);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (5, 4);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (5, 15);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (6, 5);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (6, 6);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (6, 1);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (7, 5);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (7, 1);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (8, 5);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (8, 6);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (9, 6);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (9, 1);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (10, 7);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (10, 8);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (11, 8);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (11, 9);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (12, 9);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (12, 13);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (13, 9);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (13, 14);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (14, 10);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (14, 3);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (15, 10);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (15, 11);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (15, 12);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (15, 2);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (15, 3);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (16, 12);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (16, 3);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (17, 11);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (17, 10);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (17, 2);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (18, 13);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (18, 14);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (18, 9);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (19, 13);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (19, 9);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (20, 15);
INSERT INTO PlayDatesPets(PlayDateId, PetId) VALUES (20, 4);
