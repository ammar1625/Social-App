
function ChangePassWord()
{
    return <div className="change-password-ctr">
            <div className="login-form-ctr">
            <p className="error-msg">something went wrong</p>
    
                 <form  className="login-form">
                 <p className="change-password-title">Change your password</p>
                <input type="password" className="input-field" placeholder="your current password..." />
                <input type="password" className="input-field" placeholder="your new password" />
                <input type="password" className="input-field" placeholder="comfirm your password" />
                <button className="btn login-btn">apply changes</button>
               
               {/*  <button className="btn create-account">create account</button> */}
                </form> 
            </div>
    </div> 
}

export default ChangePassWord;