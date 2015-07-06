truncate table Parts
truncate table Subassemblies


insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Bin', 'Bin', 0, 0, 0, 0);

-- Base
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base', 'Base', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (1, 2, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.A', 'Main Frame', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (2, 3, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.', 'Top Ring - 65 x 74 x 2.5 Silo Tube - 19M', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 4, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.1', 'Inner Ring - 65x2.5 SHS - 17M', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 5, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.2', 'Outer Ring - 65x2.5 SHS - 16M', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 6, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.3', '50x2.5 SHS @ 1875mm Uprights', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 7, 0, 22, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.4', '32NB @ 1875mm Trusses', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 8, 0, 22, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.5', '32NB @ 150mm ', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 9, 0, 4, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.6', '50x12 FlAT @ 150mm w hole for Auger stirrups', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 10, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.7', 'Jack retail 167.96 weld 19.50', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 11, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.', 'Weldon Mount Kit FR B9202 Jack', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 12, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.8', 'Galv Plate - Hydraulics Holder', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 13, 0, 12, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.9', '40x2.5 SHS @30', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 14, 0, 3, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.10', 'Centre Roof Lid Holder 25 x 50 SHS @ 150 Angled', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 15, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.11', 'Drawbar Base Plate 8mm', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 16, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.12', '40NB Auger Brackets notched each end w 12mm hole', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 17, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.13', '40x40x2.5 SHS @ 200 Hyd End Hose Holder w 40x10mm@20mm welded', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (3, 18, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.B', 'Axle Frame', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (2, 19, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.14', '75x4 SHS @ 1500 slotted', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (19, 20, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.15', '75x4 SHS @ 525', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (19, 21, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.16', '75x4 SHS @ 85', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (19, 22, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.17', '60T Hydraulic Holder (16mm w hole)', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (19, 23, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.18', 'Pin - 20x5mm 150mm with hole', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (19, 24, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.19', 'Hydraulic Cylinder', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (19, 25, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.20', 'Hydraulic Hoses', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (19, 26, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.C', 'Axle Gusset - Galvanised', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (2, 27, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.21', 'Axle Gusset w hole ', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 28, 0, 4, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.22', 'Axle Gusset', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 29, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.23', '75x8 FLAT @185', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 30, 0, 4, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.24', '89x89x6 @350mm', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 31, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.25', 'Stub Axle ', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 32, 0, 4, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.26', 'Hubs & Bearings', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 33, 0, 4, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.27', 'Tyre & Rim', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 34, 0, 4, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.E', 'Drawbar', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (2, 35, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.32', '100x50x5 RHS @ 1360mm with angle cut', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 36, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.33', '100x50x2mm Galv cap', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 37, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.35', 'Tongue - 100x16mm @190mm w 16mm hole', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 38, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.', '50x12mm FLAT @ 180mm holes either side', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 39, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.D', 'A-Frame Galvanised', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (2, 40, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.28', '65x65x5 RHS @1800mm', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 41, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.29', 'Drawbar Gussets', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 42, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.30', 'Drawbar Lock Plate', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 43, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.', '75x50x5mm RHS @ 900mm', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 44, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.31', 'Pin - 50x10mm 200 with hole', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 45, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.34', 'Parobolic Spring D0154', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 46, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.36', '32NB Drawbar Brackets@ 1700mm', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 47, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.37', '180x50x10mm FLAT  w 12mm holes Spring to Drawbar holder ', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 48, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.38', '25x25x2.5 SHS @260mm Angled - PTO Holder', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 49, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.39', 'M14 x 150 Bolt', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 50, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.40', 'M14 Lock Nut', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 51, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.41', 'M14x200 Bolt', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 52, 0, 2, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Base.42', 'M14 Lock Nut', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (27, 53, 0, 2, 0);

-- Steel
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '74.6 X 65 X 2.5MM X 12M', 0, 0, 163.32, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '65 X 65 X 2.5MM X 8M', 0, 0, 87.05, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '50 X 50 X 2MM X 8M', 0, 0, 39.7, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '75 X 75 X 4MM X 8M', 0, 0, 130.58, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '89 X 89 X 6MM SHS Black', 0, 0, 197.22, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '100 X 50 X 6MM RHS', 0, 0, 160.99, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '75 X 50 X 5MM RHS X 8M', 0, 0, 112.02, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '65 X 65 X 5MM RHS X 8MM Galv', 0, 0, 131.12, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (2, '', '25NB MED X 6M GALV', 0, 0, 25.79, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (2, '', '32NB MED X 6M GALV', 0, 0, 31.62, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (2, '', '40NB MED  X 6M GALV', 0, 0, 36.3, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (3, '', '25 X 8MM FLAT ', 0, 0, 13.11, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (3, '', '50 X 10MM FLAT', 0, 0, 31.83, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (3, '', '75 X 8MM FLAT', 0, 0, 38.19, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (3, '', '100x5MM FLAT', 0, 0, 42, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (3, '', '100 X 16MM FLAT X 6M', 0, 0, 101.84, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (4, '', '1.55mm X 1200 Galv Coil', 0, 0, 1460, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (4, '', '1mm x 1200 Zinculum Coil', 0, 0, 1510, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (4, '', '1mm x 900 Zinculum Coil', 0, 0, 1510, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '1.6 X 20 X 20 SHS X 8M', 0, 0, 10.06, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '1.6 X 25 X 25 SHS X 8M', 0, 0, 12.49, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '1.6 X 25 X 50 SHS X 8M', 0, 0, 24.02, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (5, '', '2.90mm X 1220 X 2400 Galv Plate', 0, 0, 124.29, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (1, '', '200x50x2mm RHS GALV', 0, 0, 97.17, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (2, '', '80nb Welded Pipe ERW', 0, 0, 201.35, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (3, '', '50 X 10mm FLAT', 0, 0, 31.83, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (3, '', '100 X 5mm FLAT', 0, 0, 31.83, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (3, '', '75 X 8mm FLAT', 0, 0, 38.19, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (6, '', '5mm X 1220 X 2400 Sheet', 0, 0, 154.09, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (7, '', '12MM Rod', 0, 0, 7.48, 0);

-- Cone Wall Top1
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone', 'Cone', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (1, 84, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone.1', '15" Auger Box Doors 2.9mm Galv (cut 205)', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (84, 85, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone.2', '15" Auger Box - 2.9mm Galv (cut 328)', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (84, 86, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone.4', 'Henrob C05 Rivets', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (84, 87, 0, 3600, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone.5', 'Manhole', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (84, 88, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone.6', 'Lid', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (84, 89, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone.7', 'Hinge 75mm', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (84, 90, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone.8', 'Hasp & Staple 115mm', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (84, 91, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone.9', '10mm Paddlock', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (84, 92, 0, 1, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone.10', 'Coil 1.55mm X 1200', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (84, 93, 0, 0.6, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Cone.11', 'Chain Links Cut B6009', 0, 0, 0, 0);
INSERT INTO Subassemblies (AssemblyId, SubassemblyId, InheritedCost, CostContribution, Count)
  VALUES (84, 94, 0, 5, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '16', '1200mm Coil', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '17', '900mm Coil', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '13', 'Site Glasses - Square', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '14', 'Site glasses - Rectangle', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '15', 'Stickers', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '18', 'Tek Screws', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '19', 'Rivets', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Henrob Rivets', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '12', '15" Auger Shroud - 2mm (Cut 153)', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Shroud Barrel', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Auger Rain Band', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, 'Part No', 'Part Description', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Henrob Rivets', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '200mm T Hinge', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '150mm T Hinge', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Rivets', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Tek Screw ??', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '100x200x3mm Galv Plate', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '50x25x1.6 curved ~2250mm', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '50x25x1.6 @2750 15deg cut on end', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '2mm Galv Plasma (Cut 500)', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '20x20x1.6 SHS @ 3250', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Tek Screw ??', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Spring', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'M6 Bolt and Nut', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '25x25x1.6@150', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '25x3 Galv x 150 w 90deg bend at 30mm and hole', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Roof Lid', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Roof Rim', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Heavy duty Hinge 100mm bent', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Solar Light', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Swing Arm Holder 3mm Galv Plate', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '6mm Bolt', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '20x20x1.6mm SHS @ 2950', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '20x20x1.6mm SHS @ 2880', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '20x20x1.6mm SHS @ 307mm (6mm hole 15mm)', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '20x20x1.6mm SHS @ 325mm (2 holes 15mm each end)', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '20x20x1.6mm SHS @180mm (45o cut at one end)', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '3mm Galv Plate 75x75mm', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '25x25x1.6mm SHS @150mm', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '20x20x1.6 @ 180mm (?? Cut one end, 6mm hole 15mm other)', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', '6mm R-Clip', 0, 0, 0, 0);

-- Auger
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '1', 'Auger Chute', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '2', '7m x 15" x 2mm Barrel', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '3', 'Barrell Galvanised', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '4', 'Laser cut -Auger Saddle Lug', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '5', 'Laser cut - Top Mount', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '6', 'Laser cut - Base Plate', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '7', 'Steel - 50x10mm Flat 735mm', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '8', 'Steel - 100x5mm Flat 735mm pressed', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '9', 'Steel 400x200x5mm Plate', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '11', 'FL208 Block Assembly', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '12', 'F208 Block Assembly ', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '13', 'UK208 Bearing Taper Lock', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '14', 'H2308 35mm Bearing Sleeve', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '', 'Grey Silicone', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '15', 'Bolt - M12x50 Hex', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '16', 'Bolt - M12x30 Cup Head', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '21', 'End Stubs - laser cut rings', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '22', 'plain shaft', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '23', 'spline shaft', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '24', 'Spiral Steel 3m sections', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '25', 'Section Spiral Steel', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '26', '7.6m 80nb heavy steel', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '27', 'FL208 Block Assembly ', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '13', 'UK208 Bearing Taper Lock', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '14', 'H2308 35mm Bearing Sleeve', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '28', 'WB6105 60Hp PTO Shaft', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '29', 'AB6105 60Hp PTO Shaft', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '30', 'Shaft 1 3/8 6 spline each end', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '31', 'Laser cut - Spline Holder', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '32', 'Steel - 200x50x2mm RHS Galv @2850mm long', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '33', 'Steel - 200x50x2mm RHS Galv @700mm long', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '34', '75x8mm FLAT @100 w 12mm hole 30x30 from corner', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '35', 'Steel - 250x450??x1.55mm Galv Plate ', 0, 0, 0, 0);
insert into Parts (Type, Number, Description, IsOwnMake, Length, OwnCost, Cost)
  values (0, '36', 'Bolt - M12x50 Hex', 0, 0, 0, 0);

-- Add all parts to Stocks
insert Stocks (PartId, Count, CountDate, Cost)
select id, 0, GETDATE(), 0 from Parts

