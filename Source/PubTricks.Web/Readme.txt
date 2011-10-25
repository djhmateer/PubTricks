Hi

Thanks for trying out PubTricks!

Any problems email me on davemateer@gmail.com

Quickstart:

- create a SQL Server database called PubTricks_Dev  (I use full blown SQL Server so I can use Profiler, but SQL Express is fine)
- run the SQL script which is in the /Lib folder.  This was generated using SQL 10.5 (ie 2008R2)
- change your connection string in web.config if necessary



Publish to live

- I've left my azure publish settings in for reference
- I generally use webdeploy with Azure (Alt B H) 
- If you're not using Azure, kill that project and all should be fine :-)
