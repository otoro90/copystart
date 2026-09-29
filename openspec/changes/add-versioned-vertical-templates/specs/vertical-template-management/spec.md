## ADDED Requirements

### Requirement: Templates are immutable and versioned
Every vertical template release MUST have an immutable identifier, semantic version, compatible platform range, enabled modules, defaults, and migration notes.

#### Scenario: Template defaults change
- **WHEN** a new copier-service template version is published
- **THEN** existing tenants remain pinned to their current version until an explicit upgrade is approved

### Requirement: Tenant overrides retain provenance
The platform SHALL distinguish inherited template values from tenant overrides and SHALL validate values against a typed configuration schema.

#### Scenario: Tenant customizes the asset label
- **WHEN** the tenant changes `Asset` from the inherited “Equipo” label to “Máquina”
- **THEN** the override records its source while unrelated future defaults remain upgradeable

### Requirement: Template upgrades are previewable and reversible
The platform MUST calculate an upgrade preview showing additions, changes, conflicts, locked fields, and obsolete overrides before applying a new template version.

#### Scenario: New template conflicts with an override
- **WHEN** an upgrade changes a field customized by the tenant
- **THEN** the system reports the conflict and requires an explicit resolution without silently overwriting the override

### Requirement: Configuration cannot bypass domain invariants
Templates SHALL configure only registered modules, vocabulary, navigation, catalogs, forms, themes, and module-owned workflow options; they MUST NOT execute tenant scripts, SQL, arbitrary markup, or unregistered side effects.

#### Scenario: A vertical requires a new invariant
- **WHEN** automotive service needs odometer validation and vehicle-specific history
- **THEN** the behavior is delivered by a typed module and exposed as template configuration rather than embedded tenant code

### Requirement: Two reference templates validate generality
The initial template catalog SHALL include copier-service and automotive-workshop templates using the same canonical operations core.

#### Scenario: Both reference workflows are exercised
- **WHEN** representative request-to-completion scenarios run for both templates
- **THEN** shared concepts use the canonical model and structural differences are isolated in registered modules