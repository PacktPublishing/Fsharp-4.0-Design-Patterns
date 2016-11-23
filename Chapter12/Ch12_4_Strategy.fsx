open System
open System.Data

type InvoiceFormat =
| Excel
| Csv

let load (format: InvoiceFormat) (path: String) =
    printfn "loading %s" path
    let dt = new DataTable() in
    (* IMPLEMENTATION GOES HERE *)
    dt
let merge (target: string) (dt: DataTable) =
    (* IMPLEMENTATION GOES HERE *)
    ()

type ILoadVendorInvoices =
    abstract LoadInvoices: String -> DataTable
    abstract member MergeInvoices: DataTable -> unit

let LoadFedex =
    { new ILoadVendorInvoices with
        member __.LoadInvoices path = load Csv path
        member __.MergeInvoices dataTable =
            merge "Fedex" dataTable
        }

let LoadLasership =
    { new ILoadVendorInvoices with
        member __.LoadInvoices path = load Excel path
        member __.MergeInvoices dataTable =
            merge "Lasership" dataTable
        }

let importEDIData (loader: ILoadVendorInvoices) path =
    loader.LoadInvoices path |> loader.MergeInvoices

[(LoadFedex,"inv2016_8_10.csv");
 (LoadLasership,"inv2016_8_10.xlsx");
 (LoadFedex,"inv2016_8_11.csv")]
|> List.iter (fun x -> x ||> importEDIData)