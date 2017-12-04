module NumericsWSF

open ExcelDna.Integration
open ExcelDna.Documentation
open MathNet.Numerics.Interpolation

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

[<ExcelFunctionDoc(
    Name="NUM.INTERP.LINEAR",
    Category="Interpolation",
    Description="Linear interpolation",
    Remarks="Based on MathNet.Numerics.LinearSpline"
)>]
let linInterp 
        ([<ExcelArgument(Description="x values on which interpolation is based")>]
        x1:float[])

        ([<ExcelArgument(Description="y values on which interpolation is based")>]
        y1:float[]) 
        
        ([<ExcelArgument(Description="x values at which interpolated values will be calculated")>]
        x2:float[]) =

    let interp = LinearSpline.Interpolate(x1, y1)
    Array.map interp.Interpolate x2 |> packForCaller

[<ExcelFunctionDoc(
    Name="NUM.INTERP.CUBIC",
    Category="Interpolation",
    Description="Cubic natural interpolation",
    Remarks="Based on MathNet.Numerics.CubicSpline"
)>]
let cubicInterp
        ([<ExcelArgument(Description="x values on which interpolation is based")>]
        x1:float[])

        ([<ExcelArgument(Description="y values on which interpolation is based")>]
        y1:float[]) 
        
        ([<ExcelArgument(Description="x values at which interpolated values will be calculated")>]
        x2:float[]) =

    let interp = CubicSpline.InterpolateNatural(x1, y1)
    Array.map interp.Interpolate x2 |> packForCaller