

function CloseAccount()
{
    
    return <div className="close-account-ctr">
         
      <div className="close-account-dialog invisible" >
            {/* Message */}
            <p className="close-account-dialog-message">
            Do you want to close your account?
            </p>

            {/* Buttons */}
            <div className="close-account-dialog-btns-ctr">
                {/* Cancel Button */}
                <button
                    type="button"
                    className="close-account-dialog-cancel-btn"
                    
                >
                    Cancel
                </button>

                {/* Confirm Button - Facebook Blue */}
                <button
                    type="button"
                    className="close-account-dialog-comfirm-btn"
                    
                >
                    Confirm
                </button>
            </div>
      </div>
                    <div className="login-form-ctr">
                   
                    <p className="error-msg">something went wrong</p>
            
                        <form  className="login-form">
                        <p className="close-account-title">would you like to close your account?</p>
                        <input type="email" className="input-field" placeholder="email..." />
                        <input type="password" className="input-field" placeholder="password..." />
                        <button className="btn login-btn">Close Account</button>
                    
                    {/*  <button className="btn create-account">create account</button> */}
                        </form> 
                    </div>
            </div>
}

export default CloseAccount;