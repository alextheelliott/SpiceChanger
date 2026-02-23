# Automated Spice Rack Project 
Completed in accordance with UBC Mech 423 2025/26
 * Austin Chuong - 24854184 - 
 * Alex Elliott  - 99567398 - alexjaelliott@gmail.com

## Firmware
Firmware was written with C in Code Composer Studio for the MSP430FR5739 board to control the physical motion and instrumentation for the project

## Windows Form App
The UI was created in a Windows Form App (C#) and was used to control the device

### Tabs
![Screenshot with the tabs labelled.](/assets/tabs-anno.png)
1. **Request Tab** — Show's the user the state of every spice managed by the machine 
2. **Restock Tab** — Let's the user add new spices to the device and remove other's
3. **COM Tab** — Manages the software's connection to the MSP board

### Request Page
![Screenshot with the request page features labelled.](/assets/app-request-anno.png)
1. **Stored Spices** — Selectable list of stored spices, ready for the user to select and withdrawl
2. **Lending (In-Progress) Spices** — List of spices which are queued for the user to retrieve
3. **Lent Spices** — Selectable list of lent spices, ready for the user to indicate they are returning
4. **Storing (In-Progress) Spices** — List of spices which are queued for the user to return
5. **Request Button** — Button to request selected stored spice
6. **Return Button** — Button to return selected lent spice
7. **Voice Request Button** — Starts voice control with a 4-5s timeout, user can verbally ask for any spice to request

### Restock Page
![Screenshot with the restock page features labelled.](/assets/app-restock-anno.png)
1. **Spice Options** — Selectable list of spice options which can be added to the device
2. **Spice's in Device** — Selectable list of spices in the device, whether they are stored or lent, etc.
3. **Add Button** — Adds selected spice from the "Spice Options" list to the device, queues the spice for returning
4. **New Button** — Prompts the user to add a new spice to the spice options list, and adds it to the device
5. **Remove Button** — Removes the selected spice from the device

### COM Page
![Screenshot of the COM page.](/assets/app-com.png)
