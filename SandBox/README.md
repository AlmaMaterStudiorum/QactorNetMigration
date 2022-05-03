# Soluzione di test per la QactorNET

Questi progetti sono dedicati alla sperimentazione per la generazione di un compilatore per file .qak che hanno una grammatica Qactor.
La grammatica originale è presente in https://github.com/anatali/issLab2021/blob/main/it.unibo.qakactor/userDocs/Qactork.xtext.
E stata effettuata una traduzione nel formato di Antlr4 (.g4).
Il modello architetturale definitivo dovrebbe essere quello di avere un progetto QactorAntlr4 per la produzione del parser , lexer e le classi/interfaccie per il visitor , e un progetto QactorCompiler per la compilazione dei file qactor.
Nel progetto QactorCompiler il visitor sarà implementato nel file QactorVisitor.partial.cs che contiene una classe parziale contenente sono le funzionalità del visitor , le funzionalità legate per esempio al log o debugging sono nel file principale QactorVisitot.cs.  
