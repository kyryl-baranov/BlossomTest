using System.Reflection;

namespace Architecture.UnitTests;

public class UnitTest1
{
    private const string DomainAssemblyName = "BlossomTest.Domain";
    private const string PresentationAssemblyName = "BlossomTest.Presentation";
    private const string ApplicationAssemblyName = "BlossomTest.Application";
    private const string InfrastructureAssemblyName = "BlossomTest.Infrastructure";
    private const string InfrastructurePersistenceAssemblyName = "BlossomTest.Infrastructure.Persistence";

    [Fact]
    public void DomainShouldNotHaveAnyDependencies()
    {
        // Arrange
        Assembly domainAssembly = Assembly.Load(DomainAssemblyName);

        // Act
        AssemblyName[] references = domainAssembly.GetReferencedAssemblies();

        // Assert
        foreach (AssemblyName reference in references)
        {
            Assembly referencedAssembly = Assembly.Load(reference);
            string? referencedAssemblyName = referencedAssembly.GetName().Name;
            Assert.NotEqual(ApplicationAssemblyName, referencedAssemblyName);
            Assert.NotEqual(InfrastructureAssemblyName, referencedAssemblyName);
            Assert.NotEqual(PresentationAssemblyName, referencedAssemblyName);
        }
    }
    
    [Fact]
    public void ApplicationShouldOnlyDependOnDomain()
    {
        // Arrange
        Assembly applicationAssembly = Assembly.Load(ApplicationAssemblyName);

        // Act
        AssemblyName[] references = applicationAssembly.GetReferencedAssemblies();

        // Assert
        foreach (AssemblyName reference in references)
        {
            Assembly referencedAssembly = Assembly.Load(reference);
            string? referencedAssemblyName = referencedAssembly.GetName().Name;
            Assert.NotEqual(InfrastructureAssemblyName, referencedAssemblyName);
            Assert.NotEqual(PresentationAssemblyName, referencedAssemblyName);
        }
        
        Assert.Contains(references, r => r.Name == DomainAssemblyName);
    }
    
    [Fact]
    public void InfrastructureShouldOnlyDependOnApplication()
    {
        // Arrange
        Assembly infrastructureAssembly = Assembly.Load(InfrastructureAssemblyName);

        // Act
        AssemblyName[] references = infrastructureAssembly.GetReferencedAssemblies();

        // Assert
        foreach (AssemblyName reference in references)
        {
            Assembly referencedAssembly = Assembly.Load(reference);
            string? referencedAssemblyName = referencedAssembly.GetName().Name;
            Assert.NotEqual(PresentationAssemblyName, referencedAssemblyName);
        }
        
        Assert.Contains(references, r => r.Name == ApplicationAssemblyName);
    }
    
    [Fact]
    public void InfrastructurePersistenceShouldOnlyDependOnApplication()
    {
        // Arrange
        Assembly infrastructurePersistenceAssembly = Assembly.Load(InfrastructurePersistenceAssemblyName);

        // Act
        AssemblyName[] references = infrastructurePersistenceAssembly.GetReferencedAssemblies();

        // Assert
        foreach (AssemblyName reference in references)
        {
            Assembly referencedAssembly = Assembly.Load(reference);
            string? referencedAssemblyName = referencedAssembly.GetName().Name;
            Assert.NotEqual(PresentationAssemblyName, referencedAssemblyName);
        }
        
        Assert.Contains(references, r => r.Name == DomainAssemblyName);
        Assert.Contains(references, r => r.Name == ApplicationAssemblyName);
    }
}