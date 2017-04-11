using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL_008d;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

/// <summary>
/// Summary description for SerializarXml
/// </summary>
public class SerializarXmlNfe
{
    public int indice = 0;
    public X509Certificate2 certificadoDigital;

    public SerializarXmlNfe()
	{
		//
		// TODO: Add constructor logic here
		//
      certificadoDigital = getCertificate();
	}

    /// <summary>
    /// Tag principal <enviNFe>
    /// </summary>
    /// <returns></returns>
    public TEnviNFe enviarNfe()
    {

       //Objeto Principal - tag <enviNFe>
       TEnviNFe envioNfe = new TEnviNFe();
       
       envioNfe.idLote = "000000000003605";
       envioNfe.indSinc = TEnviNFeIndSinc.Item0;
       envioNfe.NFe = tnfeLista();
       envioNfe.versao = "3.10";

       
       //Retorna o objeto EnviarLoteRpsEnvio
       return envioNfe;
    }

   /// <summary>
   /// Lista de NF-e para envio
   /// </summary>
   /// <returns></returns>
   public TNFe[] tnfeLista(){
      
      TNFe[] nfe = new TNFe[1];
      nfe[1] = tnfe();
      
      return nfe;
   }

   /// <summary>
   ///  tag enviNFe
   /// </summary>
   /// <returns></returns>
   public TNFe tnfe()
   {

      TNFe nfe = new TNFe();
      nfe.infNFe = tNFeInfNFe();      
//      nfe.Signature 
      return nfe;
   }

   public SignatureType signatureType() {

      SignatureType signatureType = new SignatureType();
      signatureType.Id = "4234234234";
      signatureType.SignatureValue = signatureValueType();
      signatureType.SignedInfo = signedInfo();
      return signatureType;
   
   }

   public SignatureValueType signatureValueType() {

      SignatureValueType signatureValueType = new SignatureValueType();
      signatureValueType.Id = "5445";
      //signatureValueType.Value = certificadoDigital.RawData;

      return signatureValueType;
   }

   public SignedInfoType signedInfo() {

      SignedInfoType SignedInfoType = new SignedInfoType();
      //SignedInfoType.CanonicalizationMethod = certificadoDigital
      //SignedInfoType.Id = certificadoDigital.
      ///SignedInfoType.SignatureMethod

      return SignedInfoType;
   }


   SignedInfoTypeCanonicalizationMethod signedInfoTypeCanonicalizationMethod() {

      SignedInfoTypeCanonicalizationMethod signedInfoTypeCanonicalizationMethod = new SignedInfoTypeCanonicalizationMethod();
      //signedInfoTypeCanonicalizationMethod.Algorithm = 

      return signedInfoTypeCanonicalizationMethod;
   }

   public TNFeInfNFe tNFeInfNFe() {

      TNFeInfNFe NFeInfNFe = new TNFeInfNFe();
      
      return NFeInfNFe;
   }

   public X509Certificate2 getCertificate() {

      X509Certificate2 certificado;
      certificado = new X509Certificate2("", "", X509KeyStorageFlags.MachineKeySet);
      
      return certificado;
      
   }


   
}