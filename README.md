# todo-back
# Application url: https://localhost:5001/swagger/index.html

# default users:

# Admin: 
# username: admin@superadmin.lt
# password: Mypa77w0rdSlaptasLabai

# User:
# username: first@cowtown.com
# password: stringstringstring

# Supplemente appsettings.json file with mail settings. You can find it in AppSettings block. Functionality was testing with gmail smtp server.

Tikslas
1. Sukurti „TO-DO list“ RESTful API aplikaciją (frontend dalis neprivaloma).
Funkciniai reikalavimai
1. Autentifikavimas: naudotojas autentifikuojamas vardu (el. pašto adresas) ir slaptažodžiu (min. 12 simbolių).
2. Vartotojai skirstomi į dvi roles: admin (role1) ir user (role2).
3. Vartotojas (role2) turi savo asmeninį TO-DO užduočių sąrašą. Gali peržiūrėti esamas sąraše užduotis,
pridėti naujas, keisti ir ištrinti esamas užduotis.
4. TO-DO užduotyje nurodomas jos pavadinimas ir atlikimo statusas (atlikta/neatlikta).
5. Administratorius (role1) gali peržiūrėti visų vartotojų užduočių sąrašus ir ištrinti TO-DO užduotis.
6. Realizuoti vartotojo slaptažodžio keitimo funkcionalumą, kuris vartotojui (role2) išsiųstų elektroninį
laišką su nuoroda (API endpoint, į kurį reikia perduoti naują vartotojo slaptažodį), su kuria vartotojas galėtu pasikeisti savo prisijungimo slaptažodį. Apriboti nuorodos galiojimo laiką iki 10 minučių.

Nefunkciniai reikalavimai
1. Visi programos kūrimo etapai turi atsispindėti GIT repozitorijoje, GIT commit‘ai turi būti su tvarkingais komentarais.
2. .NET Core 3.1 framework.
3. Autentifikacijai naudojamas JWT WEB TOKEN.
4. Duomenys tvarkomi MySql duomenų bazėje naudojant EntityFramework.
5. Visi būtiniausi nustatymai (connection string, email server address ir t.t.) laikomi aplikacijos
konfigūraciniame faile.
6. API naudoja ir yra testuojamas per Swagger UI įrankius.
7. Visi pradiniai aplikacijos duomenys (naudotojai, rolės, TO-DO sąrašai) kaskart yra automatiškai
sukuriami/perkuriami startuojant aplikacijai. Pakanka 1 administratoriaus, 2 vartotojų, bent dviejų TO-
DO sąrašų.
8. Projektuojant ir realizuojant API laikytis gerųjų programavimo praktikų, OOP, SOLID principų.
Atliktą užduotį įkelti į github ir atsiųsti nuorodą arba persiųsti visą repozitoriją.
