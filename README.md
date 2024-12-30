22.11.2024
Esempio dal tutorial di learnrazorpages.com
30.12.2024
Esempio di uso dei cookies per la gestione dello stato. 
Nell'esempio lo stato è costituito da un contatore e da un nome
nella classe Stato.
I metodi EncodeToBase64 e DecodeFromBase64 prendono l'oggetto contenente
lo stato, lo trasformano in json, e lo convertono in una stringa
in Base64. ( e viceversa)
Il campo statoStr contiene la stringa codificata dello stato da 
inviare (e poi ricevere) con il cookie.
Questo metodo funziona bene anche se si cambia pagina, perchè il cookie
resta in essere anche cambiando pagina. 