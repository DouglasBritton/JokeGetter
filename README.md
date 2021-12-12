# What is it
An application that retrieves jokes from HumorAPI and can find the least similar joke out of the jokes retrieved.

Please note that due to a restriction on api calls (from HumorAPI) you can only get 10 jokes a day.

# To build and run
1) Install Visual Studio 2022 with the '.NET desktop development' option selected.
2) Clone the repo and then open JokeGetter.sln.
3) Ensure that the NuGet packages have been installed for the JokeGetterCore project.
4) Create an App.config file assocaited with the JokeGetterGUI project with the content
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="humorApiKey" value="Enter api key here"/>
	</appSettings>
</configuration>
```
An api key can be created by going to https://humorapi.com/ and creating an account.

5) Ensure that JokeGetterGUI is the project selected to build.
6) Build and then run.