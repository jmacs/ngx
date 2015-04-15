using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NgxLib.Serialization;

namespace NgxLib
{
    /// <summary>
    /// The state data for one aspect of an entity.
    /// Components should not contain any behavior and should
    /// only contain primitive, serializable members.
    /// </summary>
    public abstract class NgxComponent : IRenewable, IXmlSerializable
    {
        /// <summary>
        /// Gets the mask for this component.
        /// </summary>
        /// <returns>The component mask</returns>
        public NgxComponentMetaData GetMetaData()
        {
            return Ngx.Components.Get(GetType());
        }

        /// <summary>
        /// The entity that this component belongs to. 
        /// </summary>
        public int Entity { get; private set; }

        /// <summary>
        /// If false this component will not be updated in any system
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Bind the component to an entity. 
        /// Used internally by the component meta-system.
        /// </summary>
        public void Bind(int entity)
        {
            if (Entity > 0)
                throw new InvalidOperationException("The component is already bound to an entity. You must call Unbind() first.");

            if (entity < 1)
                throw new InvalidOperationException("Invalid entity value: " + entity);

            Entity = entity;
        }

        /// <summary>
        /// Unbinds the component from the entity
        /// Used internally by the component meta-system.
        /// </summary>
        public void Unbind()
        {
            Entity = 0;
        }

        /// <summary>
        /// Called when the component is pulled from the component pool 
        /// but has not yet been added to the database. The
        /// component should be set to a clean state.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// After the component has been initializaed and added to the database.
        /// </summary>
        public virtual void Enter()
        {

        }

        /// <summary>
        /// When the component has been marked for removal but before it 
        /// has been destroyed and released to the component pool.
        /// </summary>
        public virtual void Exit()
        {

        }

        /// <summary>
        /// When the component has been released and returned
        /// to the component pool. Reference types should be 
        /// disposed or nulled here to avoid memory leaks.
        /// </summary>
        public virtual void Destroy()
        {

        }

        /// <summary>
        /// Converts the component into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        protected virtual void Serialize(XmlWriter writer)
        {

        }

        /// <summary>
        /// Generates a component from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        protected virtual void Deserialize(XmlReader reader)
        {

        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this component.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this component.
        /// </returns>
        public override string ToString()
        {
            var type = GetType().Name;
            return string.Format("{0} {{{1}}} {2}", type, Entity, Enabled);
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates a component from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            Entity = reader.GetAttributeInt("Entity");
            Enabled = reader.GetAttributeBool("Enabled");
            Deserialize(reader);
        }

        /// <summary>
        /// Converts the component into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("entity", Entity.ToString());
            writer.WriteAttributeString("enabled", Enabled.ToString());
            Serialize(writer);
        }
    }
}