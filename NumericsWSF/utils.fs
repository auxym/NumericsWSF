module Utils

open ExcelDna.Integration

/// Reformat 1D array to Nx1 or 1xN 2D array depending on excel caller
let packForCaller vs =
    let caller = XlCall.Excel(XlCall.xlfCaller) :?> ExcelReference
    let rows = caller.RowLast - caller.RowFirst + 1
    let columns = caller.ColumnLast - caller.ColumnFirst + 1

    match (columns, rows) with
    | (c, r) when (c >= r)
        -> Array2D.init 1 (Array.length vs) (fun i j -> vs.[j]) 
    | _ 
        -> Array2D.init (Array.length vs) 1 (fun i j -> vs.[i])

let validateInputLength len input =
    match Array.length input with
    | x when x=len -> true
    | _ -> false