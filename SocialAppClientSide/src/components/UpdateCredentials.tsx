

function UpdateCredentials()
{
    return <div className="update-account-screen">
        <div className="create-account-ctr">
    
            <p className="error-msg">something went wrong</p>
    
                <form className="create-account-form">
                    <div className="create-account-title-ctr">
                        <p className="title">Edit Credentials</p>
                        <p className="header-sub-title">it's quick and easy</p>
                    </div>
    
                    <div className="name-ctr">
                        <input type="text" className="input-field name-input-field" placeholder="first name" />
                        <input type="text" className="input-field name-input-field" placeholder="last name" />
                    </div>
    
                    <div className="birthdate-ctr">
                        <p className="sub-title">birthday</p>
                        <input type="date" className="input-field birthdate-input-field" />
                    </div>
    
                    <div className="gender-field-ctr">
                        <p className="sub-title">gender</p>
    
                        <div className="gender-ctr">
                            <p className="sub-title">Male</p>
                            <input type="radio" value="M" name="gender" />
                        </div>
                        <div className="gender-ctr">
                            <p className="sub-title">Female</p>
                            <input type="radio" value="F" name="gender" />
                        </div>
                    </div>
    
                    <div className="email-ctr">
                        <input type="email" className="input-field email-phone-input-field" placeholder="e-mail"/>
                    </div>
    
                    <div className="phone-ctr">
                    <input type="number" className="input-field email-phone-input-field" placeholder="phone"/>
    
                    </div>
    
                    <div className="password-ctr">
                    <input type="text" className="input-field name-input-field update-username-input-field" placeholder="user name" />
                    {/* <input type="password" className="input-field password-phone-input-field" placeholder="password"/> */}
    
                    </div>
    
                    <button className="btn create-account-btn">submit</button>
    
                    {/* <NavLink to="#" className="forgot-password-link mt-3">already have account?</NavLink> */}
            </form>
        </div>
      
        </div>

}

export default UpdateCredentials;