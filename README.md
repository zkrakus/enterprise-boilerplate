# enterprise-boilerplate

## SSH Setup
```
  $ ssh-keygen -t ed25519 -C "<emailAddress/>" // Generate key.
  $ eval "$(ssh-agent -s)" // Start key agent.
  $ ssh-add /c/Users/<user>/.ssh/id_ed25519 // add ssh key to agent.
  $ clip < /c/Users/<user>/.ssh/id_ed25519.pub  // copy ssh key and add to git profile.
  $ ssh -T git@github.com // Test ssh auth with github.
  $ git remote add origin git@github.com:<username>/<repository>.git --add repo shh endpoint
```
