# Microsoft-House
Design and Implementation of Mobile Applications project - Filippo Pedrazzini, Emanuele Torelli - Politecnico di Milano AA 2016-2017 


# TO DO

Da implementare
- Notifiche iOS
- Mandare le notifiche una volta che è stato creato un evento
- Abilitare/disabilitare le notifiche nella schermata settings
- Aggiungere metodo park now in CarParkViewModel, scalando il parcheggio ogni volta che si utilizza il qrcode
- All'apertura del calendario il giorno corrente deve essere selezionato
- Controllare se un utente è già loggato prima di mostrare la schermata di login
- UWP renderers dei picker
- Capire come mostrare la disponibilità delle stanze all'interno della listview (non si può cambiare l'itemsource)
- Eliminare le reservations
- Aggiungere degli alert dialogs
- Aggiustare la schermata new event (titolo e descrizione dell'evento)
- Aggiustare la schermata new reservation
- Aggiustare la schermata dettagli room (labels in alto e lista reservations)

Bug
- Problemi con SyncOfflineCacheAsync()
- In UWP le date del calendario si sovrappongono
- Icone UWP sgranate

Miglioramenti
- Rimettere icone a pallini in Room e Park o qualcosa del genere
- Bottoni date circolari nel calendario
- Icone 2.0
- Sostituire bottoni statistiche parcheggio con sottolineatura
- La distanza dell'utente dalla MH viene calcolata in linea d'aria (e il tempo di percorrenza a caso), bisognerebbe far funzionare il componente TK.CustomMap che calcola in automatico il percorso
