import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { layoutAPI } from '../services/api';

const SmartFooter = () => {
  const [footerData, setFooterData] = useState(null);

  useEffect(() => {
    fetchFooterData();
  }, []);

  const fetchFooterData = async () => {
    try {
      const response = await layoutAPI.getFooter();
      setFooterData(response.data);
    } catch (error) {
      console.error('Error fetching footer data:', error);
    }
  };

  if (!footerData) {
    return (
      <footer className="bg-gray-800 text-white mt-12">
        <div className="container mx-auto px-4 py-8">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
            {[1, 2, 3, 4].map(i => (
              <div key={i} className="h-32 bg-gray-700 animate-pulse rounded"></div>
            ))}
          </div>
        </div>
      </footer>
    );
  }

  return (
    <footer className="bg-gray-800 text-white mt-12">
      <div className="container mx-auto px-4 py-12 pb-24 md:pb-12">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
          {/* Company Info */}
          <div>
            <h3 className="text-xl font-bold mb-4">{footerData.companyName}</h3>
            <p className="text-gray-400 mb-4">{footerData.description}</p>
            
            {/* Contact Information */}
            {footerData.contactInfo && (
              <div className="space-y-2 text-sm">
                {footerData.contactInfo.email && (
                  <p className="text-gray-400">
                    <span className="font-semibold text-gray-300">Email:</span> {footerData.contactInfo.email}
                  </p>
                )}
                {footerData.contactInfo.phone && (
                  <p className="text-gray-400">
                    <span className="font-semibold text-gray-300">Phone:</span> {footerData.contactInfo.phone}
                  </p>
                )}
                {footerData.contactInfo.address && (
                  <p className="text-gray-400">
                    <span className="font-semibold text-gray-300">Address:</span> {footerData.contactInfo.address}
                  </p>
                )}
                {(footerData.contactInfo.city || footerData.contactInfo.state || footerData.contactInfo.zipCode || footerData.contactInfo.country) && (
                  <p className="text-gray-400">
                    {footerData.contactInfo.city && footerData.contactInfo.city}
                    {footerData.contactInfo.city && footerData.contactInfo.state && ', '}
                    {footerData.contactInfo.state && footerData.contactInfo.state}
                    {footerData.contactInfo.state && footerData.contactInfo.zipCode && ' '}
                    {footerData.contactInfo.zipCode && footerData.contactInfo.zipCode}
                    {(footerData.contactInfo.city || footerData.contactInfo.state || footerData.contactInfo.zipCode) && footerData.contactInfo.country && ', '}
                    {footerData.contactInfo.country && footerData.contactInfo.country}
                  </p>
                )}
              </div>
            )}
          </div>

          {/* Footer Sections */}
          {footerData.sections
            .filter(section => section.isActive)
            .sort((a, b) => a.displayOrder - b.displayOrder)
            .map(section => (
              <div key={section.id}>
                <h4 className="text-lg font-semibold mb-4">{section.title}</h4>
                <ul className="space-y-2">
                  {section.links
                    .filter(link => link.isActive)
                    .sort((a, b) => a.displayOrder - b.displayOrder)
                    .map(link => (
                      <li key={link.id}>
                        <Link
                          to={link.link || '#'}
                          className="text-gray-400 hover:text-white transition-colors"
                        >
                          {link.label}
                        </Link>
                      </li>
                    ))}
                </ul>
              </div>
            ))}
        </div>

        {/* Social Links */}
        {footerData.socialLinks && footerData.socialLinks.length > 0 && (
          <div className="mt-8 pt-8 border-t border-gray-700">
            <h5 className="text-sm font-semibold text-gray-300 mb-4">Follow Us</h5>
            <div className="flex flex-wrap gap-4">
              {footerData.socialLinks
                .filter(link => link.isActive !== false)
                .sort((a, b) => a.displayOrder - b.displayOrder)
                .map((link) => (
                  <a
                    key={link.id}
                    href={link.url}
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-gray-400 hover:text-white transition-colors"
                  >
                    {link.name || 'Social Link'}
                  </a>
                ))}
            </div>
          </div>
        )}

        {/* Copyright */}
        {footerData.copyrightText && (
          <div className="mt-8 pt-8 border-t border-gray-700 text-center text-gray-400">
            <p>{footerData.copyrightText}</p>
          </div>
        )}
      </div>
    </footer>
  );
};

export default SmartFooter;
