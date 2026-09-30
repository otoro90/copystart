## ADDED Requirements

### Requirement: Sources of truth have explicit ownership
The repository SHALL define concise project context, system rules, architecture documentation, and detailed topic guides with non-overlapping authority.

#### Scenario: A developer needs the current runtime version
- **WHEN** multiple documents contain different historical versions
- **THEN** the documented authority order identifies one current source and requires stale references to be corrected

### Requirement: Material decisions are evidence-backed
Architecture proposals MUST record local evidence, authoritative external sources with access dates and versions, assumptions, alternatives, validation checks, and residual risk.

#### Scenario: A framework is selected
- **WHEN** a proposal chooses between viable frameworks
- **THEN** its design records decision criteria, rejected alternatives, falsifiable checks, and reevaluation triggers

### Requirement: Changes have executable completion gates
Every implementation change MUST identify behavior-focused tests, build or static validation, documentation updates, migration safety, and rollback evidence applicable to its scope.

#### Scenario: A change is marked complete
- **WHEN** all implementation tasks are checked
- **THEN** fresh validation output and required documentation updates exist

### Requirement: Agent guidance is scoped and tested
Project instructions and skills SHALL be short, discoverable, non-duplicative, and validated with pressure scenarios before being treated as authoritative.

#### Scenario: A new recurring workflow needs guidance
- **WHEN** ordinary project documentation does not reliably produce the required behavior
- **THEN** a narrowly triggered skill is proposed, tested before and after creation, and linked from the canonical catalog