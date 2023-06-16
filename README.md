# Hospital Management System (CareCoord)

## Inrtoduction
- I developed a Web Application with my team at the college for Managing a whole hospital system. The patient can make an appointment from his home and then go to the hospital and more...

## Description
The biggest problem facing health care centres remains today. Technical problems. There is a rare health care centre that works with a modern and organised technical system that helps it organise the schedule, payment methods, save patient data, and communicate. So we were keen to find a solution to this problem, Through a Website that contains pages for the patient that enables him to record his data, book an appointment to visit the doctor, describe the pain, and view the doctor's data, pages for the nurse that enables her to confirm the patient's reservation and conduct medical tests, and pages for the doctor that enables him to view the patient's data, write his prescription, and show the medical history for the patient and pages for admin to control the whole system.

## Why Carecoord ?
CareCoord is designed to enhance care coordination among all the participants concerned with a patient’s care, ensuring that the patient’s needs and preferences are known and communicated at the right time to the right people. Whether you are a hospital administrator, a physician, a nurse, or a social worker, Carecoord can help you deliver better care with less hassle.

## Technologies:
- Front-End:
    - HTML
    - CSS
    - JavaScript
    - BootStrap

- Back-End
    - C#
    - ASP.NET Core (7)
    - MVC Pattern
    - MS SQL Server

## Features:
- [x] The patient can register to the system and create his account.

- [x] The patient can make an appointment from home.

- [x] The system sent an email to the patient to confirm his email when he register and another email when he makes an appointment to tell him information about his appointment.

- [x] The project has an admin panel to have control over all doctors' and nurses' accounts, departments, and appointments.

- [x] Doctor can write a medicine to the patient in the prescription.

- [x] The patient can get that medicine from the pharmacy with the nurse.


## Setup
follow the following steps to run the project.

- Clone this repo on your local machine.

- Download .Net7 from [.NET 7.0](https://dotnet.microsoft.com/en-us/download)

- Restore the DataBase of the project (You can find the DataBase file.bak [here](https://drive.google.com/file/d/1_TnUfVgqPKX_jGhXDnGPRs79wpcDQlv9/view?usp=sharing)) 

- Set Your Connection String in three files
  
  - `HMSproject/appsettings.json`
  - `HMSproject/Controllers/Nurse_aymanController.cs`
  - `HMSproject/Areas/Identity/Pages/Account/Manage/DeletePersonalData.cshtml.cs`
 
### Supervisors:
  - Dr. Mai Ramadan
  - Eng. Abdelghany Adel

### Contributors:
  - [Ayman Mohamed](https://github.com/AymanYassien)
  - [Fawzy Shaker](https://github.com/fawziielfaramawii)
  - [Mostafa Youssef](https://github.com/Mostafay65)

## DataBase Diagram 
<img width="1486" alt="HMS_DB" src="https://github.com/Mohamed-Adel23/Hospital-Management-System/assets/119868046/0a3367c8-d287-4b06-a591-0ac2c6ee981e">

## ScreenShots From The Website
![11](https://github.com/Mohamed-Adel23/Hospital-Management-System/assets/119868046/b456d7f5-3cda-4d15-853c-5ec431893437)

![2](https://github.com/Mohamed-Adel23/Hospital-Management-System/assets/119868046/89e97069-3cdd-4ded-84a9-c87b1c0c2c1c)

![3](https://github.com/Mohamed-Adel23/Hospital-Management-System/assets/119868046/291629ff-50ba-4052-9a3c-8b856d659d56)

![4](https://github.com/Mohamed-Adel23/Hospital-Management-System/assets/119868046/9ad3cfd7-5b81-4018-8e2b-3f3a37ed2b1d)
