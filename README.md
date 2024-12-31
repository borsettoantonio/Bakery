22.11.2024

Esempio dal tutorial di learnrazorpages.com

31.12.2024

Esempio di uso di TempData per la gestione dello stato. 

Nell'esempio lo stato è costituito da un contatore e da un nome
nella classe Stato.

I metodi EncodeToBase64 e DecodeFromBase64 prendono l'oggetto contenente
lo stato, lo trasformano in json, e lo convertono in una stringa
in Base64. ( e viceversa)

In TempData["statoStr"] viene inserito lo stato codificato,
da cui viene ripreso nella pagina successiva.

Si potrebbe anche usare il campo statoStr con l'attributo [TempData]
per inserirlo nel dizionario TempData.

Ma ho visto che questo metodo non funziona sempre correttamente: in particolare
quando riscrivo su TempData a volte il dato viene perso.

Questo metodo funziona bene anche se si cambia pagina.

Come si vede dalla pagina Privacy.cshtml.cs se il dato nel dizionario non viene toccato,
esso permane fino alla pagina successiva.

Se invece definisco il campo con l'attributo [TempData] allora devo riscriverlo
nel dizionario nel metodo OnGet.