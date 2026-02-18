using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.REPL;

public class InputErrorManagement
{
    private static readonly Lazy<InputErrorManagement> _instance = new(() => new InputErrorManagement());

    public static InputErrorManagement Instance => _instance.Value;

    private bool _isSyntaxInputError  = false;

    private InputErrorManagement()
    {
    }

    public void ResetAllErrors()
    {
        _isSyntaxInputError = false;
    }

    public void SetIsSyntaxInputError(bool isSyntaxInputError)
    {
        _isSyntaxInputError = isSyntaxInputError;
    }
    public bool GetIsSyntaxInputError()
    {
        if (_isSyntaxInputError)
        {
            _isSyntaxInputError = false;
            return true;
        }

        return false;
    }

}
