module ExcelResult

open ExcelDna.Integration

type ExcelResult = 
    | Value of float
    | Error of ExcelError

let valueError = Error ExcelError.ExcelErrorValue

let toObject (r:ExcelResult) = r :> obj