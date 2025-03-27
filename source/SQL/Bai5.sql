CREATE TABLE "tblPublisher" (
    "PublisherCode" VARCHAR(20) PRIMARY KEY,
    "PublisherName" VARCHAR(255),
    "Address" VARCHAR(255),
    "Phone" VARCHAR(20)
);

CREATE TABLE "tblBook" (
    "BookCode" VARCHAR(20) PRIMARY KEY,
    "BookName" VARCHAR(255),
    "PublisherCode" VARCHAR(20),
    FOREIGN KEY ("PublisherCode") REFERENCES "tblPublisher"("PublisherCode")
);

select * from tblBook;

-- Chèn dữ liệu vào bảng tblPublisher
INSERT INTO "tblPublisher" ("PublisherCode", "PublisherName", "Address", "Phone") VALUES
('P02020021', 'Addison Wesley', '75 Arlington St, Suite 300, Boston, MA', '113-114-0115'),
('P02020022', 'John Wiley and Sons', '605 Third Ave, New York, NY', '113-112-0117'),
('P02020023', 'McGraw Hill', '121 Ave of The Americas, New York, NY', '113-110-0118'),
('P02020024', 'Wrox', '10475 Crosspoint Blvd., Indianapolis, IN', '114-114-0119'),
('P02020025', 'Prentice Hall PTR', '49 Sandiego, USA', '110-115-0113');

-- Chèn dữ liệu vào bảng tblBook
INSERT INTO "tblBook" ("BookCode", "BookName", "PublisherCode") VALUES
('B03212049', 'Introduction to The Design and Analysis of Algorithms', 'P02020021'),
('B03212050', 'Operating System Concepts', 'P02020022'),
('B03212051', 'Advanced Concepts in Operating Systems 6th', 'P02020023'),
('B03212052', 'Beginning XML 2nd', 'P02020024'),
('B03212053', 'Core Java 2 Volume II', 'P02020025'),
('B03212054', 'A Biography Compiled', 'P02020021'),
('B03212055', 'Academic Culture', 'P02020021'),
('B03212056', 'Achieving Broad Development', 'P02020021'),
('B03212057', 'Achieving a Productive Aging Society', 'P02020021'),
('B03212058', 'Portrait of a Marching Black', 'P02020021'),
('B03212059', 'Automatically Adaptable Software', 'P02020022'),
('B03212060', 'Problems in Psychology', 'P02020022'),
('B03212061', 'Human Relations in a Factory', 'P02020022'),
('B03212062', 'Admiral Halsey''s Story', 'P02020023'),
('B03212063', 'Theoretical and Research Perspectives', 'P02020024'),
('B03212064', 'The Adolescent in Turmoil', 'P02020021'),
('B03212065', 'Adolphus, a Tale', 'P02020024'),
('B03212066', 'Adventures', 'P02020021'),
('B03212067', 'Aerogeology', 'P02020021');

