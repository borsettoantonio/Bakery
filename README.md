22.11.2024

Esempio dal tutorial di learnrazorpages.com

01.01.2025

Esempio di uso di Sessioni per la gestione dello stato. 

Nell'esempio lo stato è costituito da un contatore e da un nome
nella classe Stato.

I metodi EncodeToBase64 e DecodeFromBase64 prendono l'oggetto contenente
lo stato, lo trasformano in json, e lo convertono in una stringa
in Base64. ( e viceversa)

In Session viene inserito lo stato codificato,
da cui viene ripreso nella pagina successiva.

Questo metodo funziona bene anche se si cambia pagina.