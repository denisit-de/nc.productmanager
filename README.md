<h1>Produktverwaltung-Applikation</h1>
    <p>Dieses Repository enthält eine kleine Produktverwaltung-Applikation, die mit einer C# WebApi und einem Angular-Frontend entwickelt wurde.</p>
    <h2>Systemvoraussetzungen</h2>
    <ul>
        <li>.NET Core SDK (Version 7 oder höher)</li>
        <li>Node.js (Version 12.x oder höher)</li>
        <li>Angular CLI (Version 17 oder höher)</li>
        <li>Visual Studio oder ein ähnlicher Code-Editor mit C# und TypeScript Unterstützung</li>
    </ul>
    <h2>Setup</h2>
    <ol>
        <li><strong>Repository klonen</strong>
            <pre><code>git clone https://github.com/denisit-de/nc.productmanager.git
cd nc.productmanager</code></pre>
        </li>
        <li><strong>Backend (WebApi) starten</strong>
            <p>Wechseln Sie in das Verzeichnis der C# WebApi:</p>
            <pre><code>cd WebApi
dotnet restore
dotnet build
dotnet run</code></pre>
            <p>Die API sollte nun unter <a href="http://localhost:8000">http://localhost:8000</a> erreichbar sein.</p>
        </li>
        <li><strong>Frontend (Angular) starten</strong>
            <p>Wechseln Sie in das Verzeichnis des Angular-Frontends:</p>
            <pre><code>cd AngularApp
npm install
ng serve</code></pre>
            <p>Die Angular-Anwendung sollte unter <a href="http://localhost:4200">http://localhost:4200</a> erreichbar sein.</p>
        </li>
    </ol>
    <h2>Nutzung</h2>
    <p>Navigieren Sie im Browser zu <a href="http://localhost:4200">http://localhost:4200</a>, um die Produktverwaltung-Applikation zu verwenden.</p>

    
