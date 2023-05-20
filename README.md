<h1 align="center" style="border-bottom: none">:cloud: Filen.NET</h1>

---

<h4 align="center">« Interact with <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen's</a> cloud storage easily with your favorite language »</h4>

<div align="center">
  
  <a href="https://github.com/NaolShow/Filen.NET/blob/main/LICENSE"><img alt="GitHub license" src="https://img.shields.io/github/license/NaolShow/Filen.NET?style=flat-square"></a>  
  <a href="https://github.com/NaolShow/Filen.NET/issues"><img alt="GitHub issues" src="https://img.shields.io/github/issues/NaolShow/Filen.NET?style=flat-square"></a>
  <a href="https://github.com/NaolShow/Filen.NET/pulls"><img alt="GitHub pull requests" src="https://img.shields.io/github/issues-pr/NaolShow/Filen.NET?style=flat-square"/></a>
  <a href="https://github.com/NaolShow/Filen.NET/commits/main"><img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/NaolShow/Filen.NET?style=flat-square"/></a>

</div>

---

*If you want to support my work, please sign up on <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a> with my referral link (that you can find all over this README).
This will grant you +10GB of storage (resulting in a total of 20GB for free), and if you buy any plan I'll get a commission on it!*

# <img src="https://filen.io/images/logo_light.svg" width="30"/> What is Filen, and Why this project?

Here's a little paragraph about <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a> made by an A.I.:

<div align="center">

  > <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a> is a cloud storage service founded in 2020 in Recklinghausen, Germany. 
  > Their mission is to provide total privacy and peace of mind when using the cloud. 
  > With over 202,000 users and 6PB+ of data stored, <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a> aims to be the highest quality cloud storage solution available. 
  > The company is led by co-founders Jan Lenczyk (CEO & CTO) and Jan Kulartz (COO & CMO), who are passionate about privacy and user experience.
  
</div>

In summary, <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a> is a relatively new (compared to competitors) but provides an **affordable** and **privacy-friendly** (thanks to **client side encryption**) solution to compete against giants like **Google Drive**/**One Drive**... (for general tech companies) or **PCloud**, **iDrive**... (for cloud storage companies)

This is why I am working on **Filen.NET**, because for me, the service is exceptional, delivering excellent performance and giving users control over their data.
And contributing to the service might help it's growth, longevity and most importantly help others users (even though my project may not make a difference about it's longevity).

However, even though <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a> looks promissing, it still lacks some important features and (as reported by some users, never happened to me but..) suffers from problems like synchronization not working reliably.
I would like to correct those issues on my side by providing a C# interface for developers to interact with <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a> but also custom clients for consumers (more about that in the next section).

# What is this project?

Currently, **Filen.NET** is under heavy development during my free time. But here's the core components that I am working on:
* **Filen.API**: Provides "*low level*" interactions with <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a>, and provides tools to encrypt/decrypt data
* **Filen**: Provides "*high level*" interactions with <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a>, and handles all the hard work (like decrypting/encrypting data...)

But in the future (when both **Filen.API** and **Filen** are ready), I would like to extend this to other products like:
* **Filen.CLI**: Provides a *command line interface* to interact with <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a>
* **Filen.Desktop**: Provides a *desktop application* to interact with <a href="https://filen.io/r/1e18845a939b53caec9ea1bb33db4a01" target="_blank">Filen</a>
* ...
