// Partial AP
open System.IO

type Processable =
| FedexFile
| OnTracFile
| BrainTreeFile
with
    override this.ToString() = match this with FedexFile -> "Fedex" | OnTracFile -> "OnTrac" | BrainTreeFile -> "BrainTree"

let BraintreeHdr = "Transaction ID,Subscription ID,Transaction Type,Transaction Status,Escrow Status,Created Datetime,Created Timezone,Settlement Date,Disbursement Date,..."
let FedexHdr = "\"Bill to Account Number\";\"Invoice Date\";\"Invoice Number\";..."
let OntracHdr = "AccountNum,InvoiceNum,Reference,ShipDate,TotalCharge,Tracking,..."

//let (|IsProcessable|_|) (stream: Stream) : Processable option =
let (|IsProcessable|_|) (stream: Stream) =
    use streamReader = new StreamReader(stream)
    let hdr = streamReader.ReadLine()
    [(Processable.BrainTreeFile,BraintreeHdr);(Processable.FedexFile,FedexHdr);(Processable.OnTracFile,OntracHdr)]
    |> List.tryFind (fun x -> (snd x) = hdr)
    |> function None -> (if hdr.StartsWith("\"1\",") then Some (Processable.OnTracFile) else None) | _ as zx -> Some (fst zx.Value)

// Usage:
//    use contents = getContents uploadedFileName // getContents returns MemoryStream containing uploaded CSV off the latter name
//    match (contents) with
//    | LoadCSVtoSQL.Tools.IsProcessable csvType -> processContents uploadedFileName csvType
//    | _ -> Logger.logger.Info (sprintf "Uploaded %s contents does not belong to processable CSV types; skipping without registering" uploadedFileName)
