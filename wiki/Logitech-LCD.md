The game can display contextual information on some (older) Logitech keyboards with built-in LCD.

## Monochrome

Character selection / map change

![loading mono](https://github.com/sliekens/gw2sdk/assets/1583241/fcf850a6-17be-4445-b7a0-9fad0ca87a69)

World completion

![world completion mono 1](https://github.com/sliekens/gw2sdk/assets/1583241/098f8025-2391-4136-9a15-5afed2eaf466)
![world completion mono 2](https://github.com/sliekens/gw2sdk/assets/1583241/ecb0082f-fbc9-441f-9fe4-23c458b300f4)

Map completion

![map completion mono 1](https://github.com/sliekens/gw2sdk/assets/1583241/165a46bd-79e3-41ed-80e7-f421251f2209)
![map completion mono 2](https://github.com/sliekens/gw2sdk/assets/1583241/6f8fcaf1-9967-4e3f-a13b-a0578af9e235)

WvW (only available when you are in a WvW map)

![wvw mono 1](https://github.com/sliekens/gw2sdk/assets/1583241/ccf910e4-ce1d-4e16-921d-e65f318faad7)
![wvw mono 2](https://github.com/sliekens/gw2sdk/assets/1583241/bb5612a1-aa3e-46cc-ace1-b1b360ec168e)
![no wvw mono](https://github.com/sliekens/gw2sdk/assets/1583241/ac483386-5bba-4d18-a212-dd61c047a6d1)

Character stats

![stats mono](https://github.com/sliekens/gw2sdk/assets/1583241/0b90c8fd-959f-44aa-a87c-77253152e183)

## Color

Character selection / map change

![loading](https://github.com/sliekens/gw2sdk/assets/1583241/58d481b7-413c-43e8-a5e8-100c1d4f00b9)

World completion (sometimes incorrectly reports 0 for waypoints, points of interest and vistas)

![world completion](https://github.com/sliekens/gw2sdk/assets/1583241/945de145-fb3d-4bba-8f06-231b39cb81ea)
![world comp glitch](https://github.com/sliekens/gw2sdk/assets/1583241/98eab08e-2db4-4f9b-b37b-506c73292d4e)

Map completion

![map completion](https://github.com/sliekens/gw2sdk/assets/1583241/c5608f1a-f08a-4b34-9eb7-ebe21dc0f08a)

WvW (only available when you are in a WvW map)

![wvw](https://github.com/sliekens/gw2sdk/assets/1583241/8a52d851-2d47-4d4c-be93-eab7feebe712)
![wvw 2](https://github.com/sliekens/gw2sdk/assets/1583241/a061f97a-7d3a-409a-8340-9cbdaa1e024e)
![not wvw](https://github.com/sliekens/gw2sdk/assets/1583241/0f7f7961-99fb-4a20-a43f-cdd0412814c1)

Character name, specialization and stats  
Specialization is only updated on map change

![character](https://github.com/sliekens/gw2sdk/assets/1583241/02b77518-6293-4f0e-9002-165a58348f9d)

Combat log

![combat log empty](https://github.com/sliekens/gw2sdk/assets/1583241/22bda44c-f977-4b6b-92c6-92463b7f1cb0)
![combat log](https://github.com/sliekens/gw2sdk/assets/1583241/d7da08bf-4edf-448c-9c47-a12f155f92fa)

It is possible to intercept this information flow. Thanks to Delta, Deathmax and Freesnöw for the following information.

The Logitech LCD SDK is available from <http://gaming.logitech.com/sdk/LCDSDK_8.57.148.zip>.

The default SDK searches the registry key `HKEY_CLASSES_ROOT\HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\{d0e790a5-01a7-49ae-ae0b-e986bdd0c21b}\ServerBinary` for the path to the DLL to load. 

The SDK contains the headers (LogitechLCDLib.h) which you would need to implement.

An example implementation exists, source: <https://discord.com/channels/384735285197537290/384735523521953792/1114274342734417942>