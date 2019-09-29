# Accelist.SecurePasswordValidator

> Actually validates user password against a list of vulnerable passwords.

## What List?

This project uses the infamous hacked RockYou password list (https://techcrunch.com/2009/12/14/rockyou-hack-security-myspace-facebook-passwords/) obtained from the amazing SecList GitHub repository (https://github.com/danielmiessler/SecLists/blob/master/Passwords/Leaked-Databases/rockyou-withcount.txt.tar.gz) to distill a high quality password blacklist. This list is not perfect, but **good enough** for rejecting some of the world's most common passwords.

There are three projects in this repository:

1. The core `Accelist.SecurePasswordValidator` contains simple functions for double-checking user password input. This project is packaged and published into the NuGet repository.

2. The unit test project `Accelist.SecurePasswordValidator.Tests`

3. And the `Accelist.SecurePasswordValidator.PasswordDistiller` Console app project which is responsible for generating the blacklist used by the library. The criterion for the password blacklist are:

    - At least 8 characters in length.

    - At least 3 occurences (count) in the password list.

## Password Complexity Analysis

https://arstechnica.com/information-technology/2015/08/cracking-all-hacked-ashley-madison-passwords-could-take-a-lifetime/

BCrypt cost 12 on 4-GPU (ATI R9 290) cracking rig speed is around 156 hashes per second.

At around 200 hashes per second with only 62 characters combination `a-zA-Z0-9`, a well-crafted 8 characters password will take around 34,594.67 years to brute force.

Even if the user only uses pure lowercase `a-z` combinations, it will take at least 33.09 years to brute force an 8 characters password.

**At this point, the odds are against the crackers.** Thus the crackers will usually use a breached / known password list to brute force the password.

With this security hazard in mind, the NIST published a new security guidelines for password in 2017: (https://nvlpubs.nist.gov/nistpubs/SpecialPublications/NIST.SP.800-63b.pdf)

- Password MUST be at least 8 characters long.

- User password maximum length MUST be at least 64 characters long.

- All Unicode and ASCII characters must be accepted.

- Password MUST NOT be truncated.

- Password composition MUST NOT be limited. (e.g. Requiring mixtures of certain character types) 

- Password MUST NOT be expired unless compromised.

- Password hint is forbidden.

- Password should be validated against a list of common passwords.

- Password should be checked for the lack of entropy. (e.g. Consecutive or repetitive characters)

- Password should be checked against context-specific words. (e.g. Username + App name)

- Password strength meter should be implemented if possible.

- Login should be rate-limited.

- Password + Salt should be hashed using the proper algorithm resistant to offline attacks. (e.g. BCrypt)

## APIs

