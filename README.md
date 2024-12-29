22.11.2024
Esempio dal tutorial di learnrazorpages.com
29.12.2024
Esempio di uso di campi nascosti per la gestione dello stato. 
Nell'esempio lo stato è costituito da un contatore e da un nome
nella classe Stato.
I metodi EncodeToBase64 e DecodeFromBase64 prendono l'oggetto contenente
lo stato, lo trasformano in json, e lo convertono in una stringa
in Base64. ( e viceversa)
Il campo statoStr contiene la stringa codificata dello stato da 
inviare (e poi ricevere) con il campo nascosto.
Questo metodo funziona bene se non si cambia pagina. 
Cambiando pagina e poi tornando a quella con il contatore si
perde lo stato. 
Allora in _Layout.cshtml passo lo stato in ViewData["stato"] e lo inserisco
come segmento degli URL che vengono chiamati.
Nelle pagine che devono ricevere lo stato metto: @page "{statoStr?}"
in modo da poterlo ricevere.
Il problema di questo metodo è che bisogna mandare avanti e indietro lo stato
per tutte le pagine del programma.